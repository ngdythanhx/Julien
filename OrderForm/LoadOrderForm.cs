using ClosedXML.Excel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderForm
{
    public class LoadOrderForm
    {
        public Dictionary<string, XLWorkbook> Workbooks => _workbooks;
        Dictionary<string, XLWorkbook> _workbooks = new Dictionary<string, XLWorkbook>();
        Dictionary<string, SheetInfo> _dictSheetInfo = new Dictionary<string, SheetInfo>();
        public List<OrderFormData> LstOrder => _lstOrder;
        List<OrderFormData> _lstOrder = new List<OrderFormData>();
        OrderFormInfo _orderFormInfo;
        public LoadOrderForm(string folderPath)
        {

        }
        public async Task<Dictionary<string, SheetInfo>> GetSheetInfo(string filepath)
        {
            var ini = new IniManager(filepath);
            var sheetInfo = new SheetInfo()
            {
                SheetID = Path.GetFileNameWithoutExtension(filepath),
                SheetName = Encoding.UTF8.GetString(Encoding.Default.GetBytes(ini.GetString("Info", "Sheet"))),
                FilePath = Encoding.UTF8.GetString(Encoding.Default.GetBytes(ini.GetString("Info", "Path"))),
            };
            Dictionary<string, SheetInfo> _dictSheetInfo = new Dictionary<string, SheetInfo>();
            if (!string.IsNullOrEmpty(sheetInfo.SheetID) && !_dictSheetInfo.ContainsKey(sheetInfo.SheetID))
            {
                _dictSheetInfo[sheetInfo.SheetID] = sheetInfo;
            }
            return _dictSheetInfo;
        }
        public async Task<(bool success, string msg, List<OrderFormData> lst)> GetOrderFormDataList(SheetInfo sheetInfo)
        {
            var sheets = _workbooks[sheetInfo.FilePath].Worksheets.ToArray();
            var sheet = sheets.FirstOrDefault(s => s.Name == sheetInfo.SheetName);
            if (sheet == null)
            {
                return (false, $"Không tìm thấy sheet '{sheetInfo.SheetName}'", null);
            }
            try
            {
                var ini = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "OrderForms", sheetInfo.SheetID + ".ini"));
                int startIndex = ini.GetInt("Info", "StartIndex");
                if (startIndex > 1)
                {
                    var lastColumnUsed = sheet.LastColumnUsed();
                    var lastRowUsedIndex = sheet.LastRowUsed().RowNumber();
                    if (startIndex > lastRowUsedIndex)
                        return (false, $"Không có dữ liệu trong sheet '{sheetInfo.SheetName}'", null);
                    var rangeRows = sheet.Range($"A{startIndex}:{lastColumnUsed.ColumnLetter()}{lastRowUsedIndex}").Rows().ToArray();
                    if (rangeRows.Length > 2)
                    {
                        var c = new
                        {
                            MaKH = ini.GetString("OrderForm", "MaKH"),
                            NgayDat = ini.GetString("OrderForm", "NgayDat"),
                            PONhuom = ini.GetString("OrderForm", "PONhuom"),
                            PONhuomMoi = ini.GetString("OrderForm", "PONhuomMoi"),
                            MaDonKH = ini.GetString("OrderForm", "MaDonKH"),
                            MaHangKH = ini.GetString("OrderForm", "MaHangKH"),
                            LieuKH = ini.GetString("OrderForm", "LieuKH"),
                            LieuSd = ini.GetString("OrderForm", "LieuSd"),
                            Kho = ini.GetString("OrderForm", "Kho"),
                            MauKH = ini.GetString("OrderForm", "MauKH"),
                            MauSd = ini.GetString("OrderForm", "MauSd"),
                            SlDat = ini.GetString("OrderForm", "SlDat"),
                            ETD = ini.GetString("OrderForm", "ETD"),
                            SlXuat = ini.GetString("OrderForm", "SlXuat"),
                            NgayXuat = ini.GetString("OrderForm", "NgayXuat"),
                            Invoice = ini.GetString("OrderForm", "Invoice"),
                            Article = ini.GetString("OrderForm", "Article"),
                            ThuongHieu = ini.GetString("OrderForm", "Brand"),
                            DonGia = ini.GetString("OrderForm", "DonGia"),
                            Amount = ini.GetString("OrderForm", "Amount"),
                        };
                        var lst = new ConcurrentBag<OrderFormData>();
                        ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                        Parallel.For(0, rangeRows.Length, i =>
                        {
                            var row = rangeRows[i];
                            var order = new OrderFormData()
                            {
                                MaKH = row.Cell(c.MaKH).GetString(),
                                NgayDat = row.Cell(c.NgayDat).TryGetValue<DateTime>(out var ngaydat) ? ngaydat : DateTime.MinValue,
                                PONhuom = row.Cell(c.PONhuom).GetString(),
                                PONhuomMoi = row.Cell(c.PONhuomMoi).GetString(),
                                MaHangKH = row.Cell(c.MaHangKH).GetString(),
                                MaDonKH = row.Cell(c.MaDonKH).GetString(),
                                LieuKH = row.Cell(c.LieuKH).GetString(),
                                LieuSd = row.Cell(c.LieuSd).GetString(),
                                MauKH = row.Cell(c.MauKH).GetString(),
                                MauSd = row.Cell(c.MauSd).GetString(),
                                Kho = row.Cell(c.Kho).TryGetValue<int>(out var w) ? w : -1,
                                SlDat = row.Cell(c.SlDat).TryGetValue<double>(out var slDat) ? slDat : -1,
                                ETD = row.Cell(c.ETD).TryGetValue<DateTime>(out var etd) ? etd : DateTime.MinValue,
                                SlXuat = row.Cell(c.SlXuat).TryGetValue<double>(out var slXuat) ? slXuat : -1,
                                NgayXuat = row.Cell(c.NgayXuat).TryGetValue<DateTime>(out var ngayXuat) ? ngayXuat : DateTime.MinValue,
                                Invoice = row.Cell(c.Invoice).GetString(),
                                Article = row.Cell(c.Article).GetString(),
                                ThuongHieu = row.Cell(c.ThuongHieu).GetString(),
                                DonGia = row.Cell(c.DonGia).TryGetValue<float>(out var unitPrice) ? unitPrice : -1,
                                Amount = row.Cell(c.Amount).TryGetValue<float>(out var amount) ? amount : -1,
                            };
                            if (!string.IsNullOrEmpty(order.MaKH))
                                lst.Add(order);
                        });
                        _lstOrder.AddRange(lst);
                        return (true, $"", _lstOrder);
                    }
                }
                return (false, $"Lỗi không xác định!", null);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi hệ thống: " + ex.Message, null);
            }

        }
    }
}
