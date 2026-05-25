using ClosedXML;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMTT
{
    public class Import
    {
        public bool Cheked { get; set; } = false;
        private Import_Source _source;
        private Import_Detination _detination;
        private Dictionary<string, Material> _dictMaterial;
        private Dictionary<string, Customer> _dictCustomer;
        private string _status;

        public string Status => _status;
        public Import(Import_Source import_Source, Import_Detination import_Detination, Dictionary<string, Material> dictMaterial, Dictionary<string, Customer> dictCustomer)
        {
            _source = import_Source;
            _detination = import_Detination;
            _dictMaterial = dictMaterial;
            _dictCustomer = dictCustomer;
        }
        public async Task<(bool success,string msg)> Start(IXLWorksheet sheet_source, IXLTable tbData)
        {
            var lastColumnUsed = sheet_source.LastColumnUsed();
            var lastRowUsedIndex = sheet_source.LastRowUsed().RowNumber();
            if (_source.StartIndex > lastRowUsedIndex) return (false,"");
            //var rangeRows_source = sheet_source.Range($"A{_source.StartIndex}:{lastColumnUsed.ColumnLetter()}{lastRowUsedIndex}").Rows().ToArray();
            var rangeRows_source = sheet_source.Range($"A{_source.StartIndex}:AZ{lastRowUsedIndex}").Rows().ToArray();
            int idx_PONhuom = 1;
            int idx_MaterialName = 2;
            int idx_MaterialNumer = 4;
            int idx_QtyKg = 5;
            int idx_QtyYard = 6;
            int idx_Unit = 7;
            int idx_T2Code = 9;
            int idx_T1Name = 10;
            int idx_T1Code = 11;
            int idx_DeliveryDate = 11;
            int idx_POCustomer = 11;
            int idx_ItemCode = 11;
            int idx_DeliveryInvoice = 11;
            int idx_Invoice = 11;
            int idx_Color = 11;
            int idx_Status = 11;

            var lstCheck = new List<string>();
            foreach ( var rangeRow in tbData.RowsUsed())
            {
                string t1Name = rangeRow.Cell(idx_T1Name).GetString();
                //string mtlName = rangeRow.Cell(idx_MaterialName).GetString();
                string mtlNumber = rangeRow.Cell(idx_MaterialNumer).GetString();
                string poCus = rangeRow.Cell(idx_POCustomer).GetString();
                DateTime deliveryDate = rangeRow.Cell(idx_DeliveryDate).TryGetValue<DateTime>(out DateTime deDate) ? deDate : DateTime.MinValue;
                double qty = rangeRow.Cell(idx_QtyYard).TryGetValue<double>(out double q) ? q : -1;
                if(t1Name.Length>=3 &&
                   //_dictMaterial.TryGetValue(mtlName,out Material mtl) &&
                   //mtl.Number== mtlNumber &&
                   mtlNumber.Length>=6 &&
                   poCus.Length>=3 &&
                   deliveryDate != DateTime.MinValue &&
                   qty !=-1
                   )
                {
                    string matchCode = $"{t1Name}|{mtlNumber}|{poCus}|{deliveryDate.ToString("yyMMdd")}|{Math.Round(qty, 2)}";
                    lstCheck.Add(matchCode);
                }
               
            }
            foreach (var row in rangeRows_source)
            {
                RowHandle(row);
                string customer = row.Cell(_source.Customer).GetString();
                string poNhuom = row.Cell(_source.PONhuom).GetString();
                string materialCode = row.Cell(_source.MaterialCode).GetString();
                DateTime deliveryDate = row.Cell(_source.DeliveryDate).TryGetValue<DateTime>(out DateTime deDate) ? deDate : DateTime.MinValue;
                string poCustomer = row.Cell(_source.POCustomer).GetString();
                string itemCode = row.Cell(_source.ItemCode).GetString();
                double qty = row.Cell(_source.Qty).TryGetValue<double>(out double q) ? q : -1;
                string delieryInvoice = row.Cell(_source.DelieryInvoice).GetString();
                if (customer == _source.Customer &&
                    _dictCustomer.TryGetValue(customer, out var cus) &&
                    poNhuom.Length >= 10 && 
                    _dictMaterial.TryGetValue(materialCode, out Material mtl) && 
                    deliveryDate != DateTime.MinValue &&
                    poCustomer.Length>=3 &&
                    qty >0 &&
                    delieryInvoice.Length>=3
                    )
                {
                    string materialNumber = mtl.Number;
                    string matchCode = $"{customer}|{materialNumber}|{poCustomer}|{deliveryDate.ToString("yyMMdd")}|{Math.Round(qty, 2)}";
                    if (!lstCheck.Any(x=>x==matchCode))
                    {
                        var newRow = tbData.DataRange.InsertRowsBelow(1).First();
                        newRow.Cell(idx_PONhuom).Value = poNhuom;
                        newRow.Cell(idx_MaterialName).Value = materialCode;
                        newRow.Cell(idx_MaterialNumer).Value = materialNumber;
                        newRow.Cell(idx_T1Name).Value = customer;
                        newRow.Cell(idx_T1Code).Value = 123;
                        newRow.Cell(idx_DeliveryDate).Value = deliveryDate.Date;
                        newRow.Cell(idx_POCustomer).Value = poCustomer;
                        newRow.Cell(idx_ItemCode).Value = itemCode;
                        newRow.Cell(idx_DeliveryInvoice).Value = delieryInvoice;
                        newRow.Cell(idx_Invoice).Value = cus.InvoiceType == 1 ? $"{delieryInvoice}+{poCustomer}" : $"{delieryInvoice}+{poCustomer}+{itemCode}";
                    }
                }
            }
            return (false, "");
            //var tbData = sheet.Table(_detination.TableName);
        }

        private void RowHandle(IXLRangeRow row)
        {
            string poNhuom = row.Cell(_source.PONhuom).GetString();
            string materialCode = row.Cell(_source.MaterialCode).GetString();
            DateTime deliveryDate = row.Cell(_source.DeliveryDate).TryGetValue<DateTime>(out DateTime deDate) ? deDate : DateTime.MinValue.Date;
            string poCustomer = row.Cell(_source.POCustomer).GetString();
            string itemCode = row.Cell(_source.ItemCode).GetString();
            double qty = row.Cell(_source.Qty).TryGetValue<double>(out double q) ? q : -1;
            string delieryInvoice = row.Cell(_source.DelieryInvoice).GetString();

        }
    }
    public class Import_Result
    {

    }
    public class Import_Source
    {
        public string Path { get; set; }
        public string Sheet { get; set; }
        public int StartIndex { get; set; }
        public string Customer {  get; set; }
        public string PONhuom { get; set; }
        public string MaterialCode { get; set; }
        public string DeliveryDate { get; set; }
        public string POCustomer { get; set; }
        public string ItemCode { get; set; }
        public string Qty { get; set; }
        public string DelieryInvoice { get; set; }
        public string SmttNote { get; set; }

        public Import_Source(string customer, string path, string sheet, int startIndex, string pONhuom, string materialCode, string deliveryDate, string pOCustomer, string itemCode, string qty, string delieryInvoice,string smttNote)
        {
            Customer = customer;
            Path = path;
            Sheet = sheet;
            StartIndex = startIndex;
            PONhuom = pONhuom;
            MaterialCode = materialCode;
            DeliveryDate = deliveryDate;
            POCustomer = pOCustomer;
            ItemCode = itemCode;
            Qty = qty;
            DelieryInvoice = delieryInvoice;
            SmttNote = smttNote;
        }
    }
    public class Import_Detination
    {
        public string Path { get; set; }
        public string Sheet { get; set; }
        public string TableName { get; set; }
        public Import_Detination(string path, string sheet, string tableName)
        {
            Path = path;
            Sheet = sheet;
            TableName = tableName;
        }
    }
}
