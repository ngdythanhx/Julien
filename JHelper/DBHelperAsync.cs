using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace JHelper
{
    /// <summary>
    /// Tùy chọn cho từng lệnh SQL. Thuộc tính nào không đặt sẽ dùng giá trị mặc định.
    /// </summary>
    public class DBCommandOptions
    {
        /// <summary>Text (mặc định) hoặc StoredProcedure. Hàm ...SPAsync tự đặt giá trị này.</summary>
        public CommandType CommandType { get; set; } = CommandType.Text;

        /// <summary>Timeout của lệnh (giây). Null = mặc định 30 giây.</summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// Số lần thử lại khi gặp lỗi tạm thời. Null = mặc định theo loại hàm
        /// (hàm đọc: 2 lần; hàm ghi ExecuteNonQuery/ExecuteScalar: 0 lần).
        /// </summary>
        public int? RetryCount { get; set; }

        /// <summary>Thời gian chờ giữa các lần thử (ms), tăng gấp đôi sau mỗi lần.</summary>
        public int RetryDelayMs { get; set; } = 300;

        public CancellationToken CancellationToken { get; set; } = default(CancellationToken);

        internal DBCommandOptions Clone(CommandType? commandType = null)
        {
            return new DBCommandOptions
            {
                CommandType = commandType ?? CommandType,
                Timeout = Timeout,
                RetryCount = RetryCount,
                RetryDelayMs = RetryDelayMs,
                CancellationToken = CancellationToken
            };
        }
    }

    /// <summary>
    /// Cách gọi:
    ///   await db.GetListAsync&lt;User&gt;("SELECT * FROM Users WHERE Id=@Id", new SqlParameter("@Id", 1));
    ///   await db.GetListSPAsync&lt;User&gt;("sp_GetUsers", new SqlParameter("@Id", 1));
    ///   await db.ExecuteNonQueryAsync(sql, new DBCommandOptions { Timeout = 120, RetryCount = 3 }, p1, p2);
    /// </summary>
    public class DBHelperAsync
    {
        private const int DefaultCommandTimeout = 30;
        private const int DefaultReadRetry = 2;
        private const int DefaultWriteRetry = 0;

        private readonly string _connectionString;          // có pooling
        private readonly string _fallbackConnectionString;  // không pooling (dự phòng)

        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> _propertyCache
            = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        private static readonly object _cacheLock = new object();

        // Mã lỗi SQL Server được coi là tạm thời (có thể thử lại)
        private static readonly HashSet<int> _transientErrors = new HashSet<int>
        {
            -2,     // timeout
            53, 64, 233,
            1205,   // deadlock
            4060,
            10053, 10054, 10060,
            10928, 10929,
            40197, 40501, 40613,
            49918, 49919, 49920
        };

        public DBHelperAsync(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string can not null", nameof(connectionString));
            }

            _connectionString = FixConnectionString(connectionString, true);
            _fallbackConnectionString = FixConnectionString(connectionString, false);
        }

        public static string FixConnectionString(string connectionString, bool pooling)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);

            if (pooling)
            {
                builder.Pooling = true;
                builder.MinPoolSize = 5;
                builder.MaxPoolSize = 100;
                builder.ConnectTimeout = 5;
            }
            else
            {
                builder.Pooling = false;
                builder.MinPoolSize = 0;
                builder.ConnectTimeout = 10;
            }

            return builder.ConnectionString;
        }

        public async Task<SqlConnection> OpenConnectionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var connection = new SqlConnection(_connectionString);

            try
            {
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                return connection;
            }
            catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException)
            {
                connection.Dispose();
            }
            catch
            {
                connection.Dispose();
                throw;
            }

            cancellationToken.ThrowIfCancellationRequested();

            var fallback = new SqlConnection(_fallbackConnectionString);

            try
            {
                await fallback.OpenAsync(cancellationToken).ConfigureAwait(false);
                return fallback;
            }
            catch
            {
                fallback.Dispose();
                throw;
            }
        }

        #region Core (timeout + retry)

        private static SqlCommand CreateCommand(
            SqlConnection connection,
            string sql,
            DBCommandOptions options,
            SqlParameter[] parameters)
        {
            var command = new SqlCommand(sql, connection)
            {
                CommandType = options.CommandType,
                CommandTimeout = options.Timeout ?? DefaultCommandTimeout
            };

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }

        /// <summary>
        /// Chạy một lệnh: mở connection, tạo command, thực thi, tự thử lại khi gặp lỗi tạm thời.
        /// keepOpen = true dành cho ExecuteReader (connection được đóng khi caller Dispose reader).
        /// </summary>
        private async Task<TResult> RunAsync<TResult>(
            string sql,
            DBCommandOptions options,
            SqlParameter[] parameters,
            int defaultRetry,
            bool keepOpen,
            Func<SqlCommand, CancellationToken, Task<TResult>> action)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("SQL / stored procedure name can not null", nameof(sql));
            }

            options = options ?? new DBCommandOptions();
            var ct = options.CancellationToken;
            int maxRetry = Math.Max(0, options.RetryCount ?? defaultRetry);
            int attempt = 0;

            while (true)
            {
                // SqlParameter chỉ gắn được vào 1 command -> từ lần thử thứ 2 phải dùng bản sao
                var args = attempt == 0 ? parameters : CloneParameters(parameters);

                SqlConnection connection = null;
                SqlCommand command = null;
                bool handedOver = false;

                try
                {
                    connection = await OpenConnectionAsync(ct).ConfigureAwait(false);
                    command = CreateCommand(connection, sql, options, args);

                    var result = await action(command, ct).ConfigureAwait(false);

                    if (!ReferenceEquals(args, parameters))
                    {
                        CopyBackOutputs(args, parameters);
                    }

                    handedOver = keepOpen;
                    return result;
                }
                catch (Exception ex) when (attempt < maxRetry && !ct.IsCancellationRequested && IsTransient(ex))
                {
                    attempt++;
                }
                finally
                {
                    if (!handedOver)
                    {
                        command?.Dispose();
                        connection?.Dispose();
                    }
                }

                // Chỉ tới đây khi vừa bắt lỗi tạm thời -> chờ rồi thử lại
                int delay = options.RetryDelayMs * (1 << Math.Min(attempt - 1, 10));
                await Task.Delay(delay, ct).ConfigureAwait(false);
            }
        }

        private static bool IsTransient(Exception ex)
        {
            if (ex is OperationCanceledException)
            {
                return false;
            }

            if (ex is TimeoutException)
            {
                return true;
            }

            var sqlEx = ex as SqlException;
            if (sqlEx == null)
            {
                return false;
            }

            foreach (SqlError error in sqlEx.Errors)
            {
                if (_transientErrors.Contains(error.Number))
                {
                    return true;
                }
            }

            return false;
        }

        private static SqlParameter[] CloneParameters(SqlParameter[] source)
        {
            if (source == null)
            {
                return null;
            }

            return source
                .Select(p => (SqlParameter)((ICloneable)p).Clone())
                .ToArray();
        }

        // Trả giá trị OUTPUT / RETURN từ bản sao về lại tham số gốc của người gọi
        private static void CopyBackOutputs(SqlParameter[] clones, SqlParameter[] originals)
        {
            if (clones == null || originals == null)
            {
                return;
            }

            for (int i = 0; i < clones.Length && i < originals.Length; i++)
            {
                if (originals[i].Direction != ParameterDirection.Input)
                {
                    originals[i].Value = clones[i].Value;
                }
            }
        }

        #endregion

        #region ExecuteNonQuery

        public Task<int> ExecuteNonQueryAsync(string sql, params SqlParameter[] parameters)
        {
            return ExecuteNonQueryAsync(sql, (DBCommandOptions)null, parameters);
        }

        public Task<int> ExecuteNonQueryAsync(string sql, DBCommandOptions options, params SqlParameter[] parameters)
        {
            return RunAsync(sql, options, parameters, DefaultWriteRetry, false,
                (cmd, ct) => cmd.ExecuteNonQueryAsync(ct));
        }

        public Task<int> ExecuteNonQuerySPAsync(string spName, params SqlParameter[] parameters)
        {
            return ExecuteNonQuerySPAsync(spName, (DBCommandOptions)null, parameters);
        }

        public Task<int> ExecuteNonQuerySPAsync(string spName, DBCommandOptions options, params SqlParameter[] parameters)
        {
            return ExecuteNonQueryAsync(
                spName,
                (options ?? new DBCommandOptions()).Clone(CommandType.StoredProcedure),
                parameters);
        }

        #endregion

        #region ExecuteScalar

        public Task<object> ExecuteScalarAsync(string sql, params SqlParameter[] parameters)
        {
            return ExecuteScalarAsync(sql, (DBCommandOptions)null, parameters);
        }

        public Task<object> ExecuteScalarAsync(string sql, DBCommandOptions options, params SqlParameter[] parameters)
        {
            return RunAsync(sql, options, parameters, DefaultWriteRetry, false,
                (cmd, ct) => cmd.ExecuteScalarAsync(ct));
        }

        public Task<object> ExecuteScalarSPAsync(string spName, params SqlParameter[] parameters)
        {
            return ExecuteScalarSPAsync(spName, (DBCommandOptions)null, parameters);
        }

        public Task<object> ExecuteScalarSPAsync(string spName, DBCommandOptions options, params SqlParameter[] parameters)
        {
            return ExecuteScalarAsync(
                spName,
                (options ?? new DBCommandOptions()).Clone(CommandType.StoredProcedure),
                parameters);
        }

        #endregion

        #region ExecuteReader

        /// <summary>
        /// Trả về SqlDataReader đang mở; đóng reader sẽ đóng luôn connection.
        /// Người gọi PHẢI Dispose reader. Retry chỉ áp dụng cho bước mở và thực thi lệnh,
        /// không áp dụng khi đang đọc dữ liệu.
        /// </summary>
        public Task<SqlDataReader> ExecuteReaderAsync(string sql, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsync(sql, (DBCommandOptions)null, parameters);
        }

        public Task<SqlDataReader> ExecuteReaderAsync(string sql, DBCommandOptions options, params SqlParameter[] parameters)
        {
            return RunAsync(sql, options, parameters, DefaultReadRetry, true,
                (cmd, ct) => cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct));
        }

        public Task<SqlDataReader> ExecuteReaderSPAsync(string spName, params SqlParameter[] parameters)
        {
            return ExecuteReaderSPAsync(spName, (DBCommandOptions)null, parameters);
        }

        public Task<SqlDataReader> ExecuteReaderSPAsync(string spName, DBCommandOptions options, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsync(
                spName,
                (options ?? new DBCommandOptions()).Clone(CommandType.StoredProcedure),
                parameters);
        }

        #endregion

        #region GetDataTable

        public Task<DataTable> GetDataTableAsync(string sql, params SqlParameter[] parameters)
        {
            return GetDataTableAsync(sql, (DBCommandOptions)null, parameters);
        }

        public Task<DataTable> GetDataTableAsync(string sql, DBCommandOptions options, params SqlParameter[] parameters)
        {
            return RunAsync(sql, options, parameters, DefaultReadRetry, false, async (cmd, ct) =>
            {
                var dataTable = new DataTable();

                using (var reader = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false))
                {
                    dataTable.Load(reader);
                }

                return dataTable;
            });
        }

        public Task<DataTable> GetDataTableSPAsync(string spName, params SqlParameter[] parameters)
        {
            return GetDataTableSPAsync(spName, (DBCommandOptions)null, parameters);
        }

        public Task<DataTable> GetDataTableSPAsync(string spName, DBCommandOptions options, params SqlParameter[] parameters)
        {
            return GetDataTableAsync(
                spName,
                (options ?? new DBCommandOptions()).Clone(CommandType.StoredProcedure),
                parameters);
        }

        #endregion

        #region GetList

        public Task<List<T>> GetListAsync<T>(string sql, params SqlParameter[] parameters)
            where T : new()
        {
            return GetListAsync<T>(sql, (DBCommandOptions)null, parameters);
        }

        public Task<List<T>> GetListAsync<T>(string sql, DBCommandOptions options, params SqlParameter[] parameters)
            where T : new()
        {
            return RunAsync(sql, options, parameters, DefaultReadRetry, false, async (cmd, ct) =>
            {
                var list = new List<T>();

                using (var reader = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false))
                {
                    var propertyMap = GetPropertyMap(typeof(T));

                    // Ánh xạ cột -> property một lần, không lặp lại cho mỗi dòng
                    var columns = new PropertyInfo[reader.FieldCount];
                    var targetTypes = new Type[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo property;
                        if (propertyMap.TryGetValue(reader.GetName(i), out property))
                        {
                            columns[i] = property;
                            targetTypes[i] = Nullable.GetUnderlyingType(property.PropertyType)
                                             ?? property.PropertyType;
                        }
                    }

                    while (await reader.ReadAsync(ct).ConfigureAwait(false))
                    {
                        var item = new T();

                        for (int i = 0; i < columns.Length; i++)
                        {
                            var property = columns[i];

                            if (property == null || reader.IsDBNull(i))
                            {
                                continue;
                            }

                            property.SetValue(item, ConvertValue(reader.GetValue(i), targetTypes[i]));
                        }

                        list.Add(item);
                    }
                }

                return list;
            });
        }

        public Task<List<T>> GetListSPAsync<T>(string spName, params SqlParameter[] parameters)
            where T : new()
        {
            return GetListSPAsync<T>(spName, (DBCommandOptions)null, parameters);
        }

        public Task<List<T>> GetListSPAsync<T>(string spName, DBCommandOptions options, params SqlParameter[] parameters)
            where T : new()
        {
            return GetListAsync<T>(
                spName,
                (options ?? new DBCommandOptions()).Clone(CommandType.StoredProcedure),
                parameters);
        }

        private static Dictionary<string, PropertyInfo> GetPropertyMap(Type type)
        {
            lock (_cacheLock)
            {
                Dictionary<string, PropertyInfo> map;

                if (!_propertyCache.TryGetValue(type, out map))
                {
                    map = type
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => p.CanWrite && p.GetIndexParameters().Length == 0)
                        .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

                    _propertyCache[type] = map;
                }

                return map;
            }
        }

        private static object ConvertValue(object value, Type targetType)
        {
            if (targetType.IsInstanceOfType(value))
            {
                return value;
            }

            if (targetType.IsEnum)
            {
                var text = value as string;
                return text != null
                    ? Enum.Parse(targetType, text, true)
                    : Enum.ToObject(targetType, value);
            }

            if (targetType == typeof(Guid))
            {
                var text = value as string;
                return text != null ? Guid.Parse(text) : (Guid)value;
            }

            return Convert.ChangeType(value, targetType);
        }

        #endregion

        #region GetInstance

        public Task<T> GetInstanceAsync<T>(string sql, params SqlParameter[] parameters)
            where T : new()
        {
            return GetInstanceAsync<T>(sql, (DBCommandOptions)null, parameters);
        }

        public async Task<T> GetInstanceAsync<T>(string sql, DBCommandOptions options, params SqlParameter[] parameters)
            where T : new()
        {
            var list = await GetListAsync<T>(sql, options, parameters).ConfigureAwait(false);

            return list.FirstOrDefault();
        }

        public Task<T> GetInstanceSPAsync<T>(string spName, params SqlParameter[] parameters)
            where T : new()
        {
            return GetInstanceSPAsync<T>(spName, (DBCommandOptions)null, parameters);
        }

        public Task<T> GetInstanceSPAsync<T>(string spName, DBCommandOptions options, params SqlParameter[] parameters)
            where T : new()
        {
            return GetInstanceAsync<T>(
                spName,
                (options ?? new DBCommandOptions()).Clone(CommandType.StoredProcedure),
                parameters);
        }

        #endregion
    }
}