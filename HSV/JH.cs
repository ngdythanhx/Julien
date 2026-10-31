using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSV
{
    public class JH
    {
        private async Task<(bool success, string msg)> CreateINVHD(string cusName, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("INV HĐ");
                    //sheet.Cell("E3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    var lstINVHD = lstINV.Where(x => !x.PO.ToUpper().Contains("CLAIM") && !x.PO.ToUpper().Contains("FOC") && x.UnitPriceUSD > 0).GroupBy(x => x.Description).Select(x => new INVHD()
                    {
                        DESCRIPTION = x.First().Description,
                        COLOR = x.Sum(y => y.Qty).ToString("#,##0.000"),
                        SOLUONG = x.Sum(y => y.Qty),
                        DONGIA = x.First().UnitPriceVND
                    }).OrderBy(x => x.DESCRIPTION).ToList();
                    if (lstINVHD.Count == 0)
                        return (true, "");
                    sheet.Row(19).InsertRowsBelow(lstINVHD.Count);
                    for (int i = 0; i < lstINVHD.Count; i++)
                    {
                        var row = sheet.Range($"A{i + 20}:F{i + 20}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Cell("O").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Cell("O").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        row.Cell("A").Value = (i + 1).ToString();
                        row.Cell("B").Value = cusName == "HSV" ? $"{lstINVHD[i].DESCRIPTION}\nHS CODE : 60069000" : lstINVHD[i].DESCRIPTION;
                        row.Cell("C").Value = lstINVHD[i].COLOR;
                        row.Cell("D").Value = lstINVHD[i].SOLUONG;
                        row.Cell("E").Value = lstINVHD[i].DONGIA;
                        row.Cell("F").FormulaA1 = $"=ROUND(D{i + 20}*E{i + 20},0)";
                        row.Cell("F").Style.NumberFormat.Format = "#,##0.00";
                        row.Cell("F").Style.Font.Bold = true;
                    }
                    sheet.Row(20 + lstINVHD.Count).Cell("D").FormulaA1 = $"=SUM(D20:D{19 + lstINVHD.Count})";
                    sheet.Row(20 + lstINVHD.Count).Cell("F").FormulaA1 = $"=SUM(F20:F{19 + lstINVHD.Count})";
                    sheet.Row(21 + lstINVHD.Count).Cell("F").FormulaA1 = $"=ROUND(F{20 + lstINVHD.Count}*0.08,0)";
                    sheet.Row(22 + lstINVHD.Count).Cell("F").FormulaA1 = $"=SUM(F{20 + lstINVHD.Count}+F{21 + lstINVHD.Count})";
                    var rowEnd = sheet.Range($"C{24 + lstINVHD.Count}:F{24 + lstINVHD.Count}");
                    rowEnd.Merge();
                    sheet.Cell($"C{24 + lstINVHD.Count}").Value = "JULIEN (VN) METAL POWDER CO.,LTD";
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreatePKLHD(string cusName, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("PKL HĐ");
                    //sheet.Cell("I3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    var lstPKLs = lstINV.Where(x => !x.PO.ToUpper().Contains("CLAIM") && !x.PO.ToUpper().Contains("FOC") && x.UnitPriceUSD > 0).GroupBy(x => new { x.Description, x.Thickness }).OrderBy(g => g.Key.Description).Select(a =>
                        a.Select(y => new PKLHD()
                        {
                            PO = y.PO,
                            BuyMonth = y.BuyMonth,
                            Article = y.Article,
                            MaterialCode = y.MaterialCode,
                            DyeLot = y.DyeLot,
                            //DyeLot = y.DyeLot.Contains("\n") ? $"\"{y.DyeLot}\"" : y.DyeLot,
                            HSCode = y.HSCode,
                            SaleNo = y.SaleNo,
                            Description = y.Description,
                            Color = y.Color,
                            Thickness = y.Thickness,
                            Qty = y.Qty,
                            Roll = y.Qty,
                        }).ToList()
                    ).ToList();
                    var rowCount = lstPKLs.Sum(a => a.Count);
                    int n = 20;
                    var lstIndex = new List<int>();
                    foreach (var lstPKL in lstPKLs)
                    {
                        for (int i = 0; i < lstPKL.Count; i++)
                        {
                            sheet.Row(n++).InsertRowsBelow(1);
                            var row = sheet.Range($"A{n}:O{n}").Row(1);
                            row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            row.Cell("M").Style.NumberFormat.Format = "#,##0";
                            row.Style.Font.Bold = false;
                            var item = lstPKL[i];

                            row.Cell("A").Value = lstPKL[i].PO;
                            row.Cell("B").Value = lstPKL[i].BuyMonth;
                            row.Cell("C").Value = lstPKL[i].Article;
                            row.Cell("D").Value = lstPKL[i].MaterialCode;
                            row.Cell("E").Value = lstPKL[i].DyeLot;
                            //row.Cell("E").Value = lstPKL[i].DyeLot.Contains("\n") ? $"\"{lstPKL[i].DyeLot}\"" : lstPKL[i].DyeLot;
                            row.Cell("F").Value = lstPKL[i].HSCode;
                            row.Cell("G").Value = lstPKL[i].Description;
                            row.Cell("H").Value = lstPKL[i].Color;
                            row.Cell("I").Value = lstPKL[i].Thickness;
                            row.Cell("J").Value = lstPKL[i].Qty;
                            row.Cell("J").Style.NumberFormat.Format = "#,##0.000";
                            row.Cell("K").Value = lstPKL[i].Roll;
                            row.Cell("K").Style.NumberFormat.Format = "#,##0.000";
                            row.Cell("L").Value = lstPKL[i].Unit;
                            row.Cell("M").Value = 1;
                            row.Cell("N").FormulaA1 = $"=J{n}*0.24";
                            row.Cell("O").FormulaA1 = $"=N{n}+0.2";
                            if (i == lstPKL.Count - 1)
                            {
                                sheet.Row(n++).InsertRowsBelow(1);
                                row = sheet.Range($"A{n}:O{n}").Row(1);
                                row.Style.Font.Bold = true;
                                sheet.Range($"A{n}:I{n}").Merge();
                                row.Cell("A").Value = "TOTAL:";
                                row.Cell("J").Value = lstPKL.Sum(x => x.Qty);
                                row.Cell("L").Value = "YARD";
                                row.Cell("M").Value = lstPKL.Count;
                                row.Cell("M").Style.NumberFormat.Format = "#,##0\" Roll\"";
                                row.Cell("N").FormulaA1 = $"=SUM(N{n - lstPKL.Count}:N{n - 1})";
                                row.Cell("O").FormulaA1 = $"=SUM(O{n - lstPKL.Count}:O{n - 1})";
                                lstIndex.Add(n);
                            }

                        }
                    }
                    sheet.Row(n++).InsertRowsBelow(1);
                    var rowEnd = sheet.Range($"A{n}:O{n}").Row(1);
                    sheet.Range($"A{n}:I{n}").Merge();
                    rowEnd.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Cell("A").Value = "TOTAL:";
                    rowEnd.Cell("J").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "J" + x))})";
                    rowEnd.Cell("L").Value = "YARD";
                    rowEnd.Cell("M").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "M" + x))})";
                    rowEnd.Cell("N").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "N" + x))})";
                    rowEnd.Cell("O").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "O" + x))})";
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
    }
}
