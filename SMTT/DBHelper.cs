using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMTT

{
    public class DBHelper
    {
        private readonly string _connectionString;

        public DBHelper(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string can not null", nameof(connectionString));
            }

            _connectionString = FixConnectionString(connectionString, true);
        }

        public static string FixConnectionString(string connectionString, bool pooling)
        {
            var parts = connectionString
                .Split(';')
                .Where(x =>
                    !x.StartsWith("pooling=", StringComparison.OrdinalIgnoreCase) &&
                    !x.StartsWith("min pool size=", StringComparison.OrdinalIgnoreCase) &&
                    !x.StartsWith("max pool size=", StringComparison.OrdinalIgnoreCase) &&
                    !x.StartsWith("connect timeout=", StringComparison.OrdinalIgnoreCase));

            var result = string.Join(";", parts) + ";";

            if (pooling)
            {
                result += "Pooling=true;Min Pool Size=5;Max Pool Size=100;Connect Timeout=5;";
            }
            else
            {
                result += "Pooling=false;Connect Timeout=10;";
            }

            return result;
        }

        public async Task<SqlConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync(cancellationToken);

                return connection;
            }
            catch
            {
                var connection = new SqlConnection(FixConnectionString(_connectionString, false));

                await connection.OpenAsync(cancellationToken);

                return connection;
            }
        }

        #region ExecuteNonQuery

        public async Task<int> ExecuteNonQueryAsync(
            string sql,
            CommandType commandType = CommandType.Text,
            int timeout = 30,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
        {
             using var connection = await OpenConnectionAsync(cancellationToken);

            using var command = new SqlCommand(sql, connection)
            {
                CommandType = commandType,
                CommandTimeout = timeout
            };

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            return await command.ExecuteNonQueryAsync(cancellationToken);
        }

        public async Task<int> ExecuteNonQuerySPAsync(
            string spName,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
        {
            return await ExecuteNonQueryAsync(
                spName,
                CommandType.StoredProcedure,
                30,
                cancellationToken,
                parameters);
        }

        #endregion

        #region ExecuteScalar

        public async Task<object> ExecuteScalarAsync(
            string sql,
            CommandType commandType = CommandType.Text,
            int timeout = 30,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
        {
            using var connection = await OpenConnectionAsync(cancellationToken);

            using var command = new SqlCommand(sql, connection)
            {
                CommandType = commandType,
                CommandTimeout = timeout
            };

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            return await command.ExecuteScalarAsync(cancellationToken);
        }

        public async Task<object> ExecuteScalarSPAsync(
            string spName,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
        {
            return await ExecuteScalarAsync(
                spName,
                CommandType.StoredProcedure,
                30,
                cancellationToken,
                parameters);
        }

        #endregion

        #region ExecuteReader

        public async Task<SqlDataReader> ExecuteReaderAsync(
            string sql,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
        {
            var connection = await OpenConnectionAsync(cancellationToken);

            try
            {
                var command = new SqlCommand(sql, connection)
                {
                    CommandType = commandType
                };

                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                return await command.ExecuteReaderAsync(
                    CommandBehavior.CloseConnection,
                    cancellationToken);
            }
            catch
            {
                connection.Dispose();
                throw;
            }
        }

        public async Task<SqlDataReader> ExecuteReaderSPAsync(
            string spName,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
        {
            return await ExecuteReaderAsync(
                spName,
                CommandType.StoredProcedure,
                cancellationToken,
                parameters);
        }

        #endregion

        #region GetDataTable

        public async Task<DataTable> GetDataTableAsync(
            string sql,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
        {
            var dataTable = new DataTable();

            using var connection = await OpenConnectionAsync(cancellationToken);

            using var command = new SqlCommand(sql, connection)
            {
                CommandType = commandType
            };

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            dataTable.Load(reader);

            return dataTable;
        }

        public async Task<DataTable> GetDataTableSPAsync(
            string spName,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
        {
            return await GetDataTableAsync(
                spName,
                CommandType.StoredProcedure,
                cancellationToken,
                parameters);
        }

        #endregion

        #region GetList

        public async Task<List<T>> GetListAsync<T>(
            string sql,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
            where T : new()
        {
            var list = new List<T>();

            using var connection = await OpenConnectionAsync(cancellationToken);

            using var command = new SqlCommand(sql, connection)
            {
                CommandType = commandType
            };

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var propertyMap = properties
                .ToDictionary(x => x.Name, x => x, StringComparer.OrdinalIgnoreCase);

            while (await reader.ReadAsync(cancellationToken))
            {
                var item = new T();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = reader.GetName(i);

                    if (!propertyMap.TryGetValue(columnName, out var property))
                    {
                        continue;
                    }

                    if (reader.IsDBNull(i))
                    {
                        continue;
                    }

                    var value = reader.GetValue(i);

                    var targetType = Nullable.GetUnderlyingType(property.PropertyType)
                                     ?? property.PropertyType;

                    value = Convert.ChangeType(value, targetType);

                    property.SetValue(item, value);
                }

                list.Add(item);
            }

            return list;
        }

        public async Task<List<T>> GetListSPAsync<T>(
            string spName,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
            where T : new()
        {
            return await GetListAsync<T>(
                spName,
                CommandType.StoredProcedure,
                cancellationToken,
                parameters);
        }

        #endregion

        #region GetInstance

        public async Task<T> GetInstanceAsync<T>(
            string sql,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
            where T : new()
        {
            var list = await GetListAsync<T>(
                sql,
                commandType,
                cancellationToken,
                parameters);

            return list.FirstOrDefault();
        }

        public async Task<T> GetInstanceSPAsync<T>(
            string spName,
            CancellationToken cancellationToken = default,
            params SqlParameter[] parameters)
            where T : new()
        {
            return await GetInstanceAsync<T>(
                spName,
                CommandType.StoredProcedure,
                cancellationToken,
                parameters);
        }

        #endregion
    }
}
