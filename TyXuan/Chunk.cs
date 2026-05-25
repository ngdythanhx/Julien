using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Geom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Geom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

public class Chunk
{
    public string Text;
    public float X;
    public float Y;
}

public class Strategy : IEventListener
{
    public List<Chunk> Chunks = new List<Chunk>();

    public void EventOccurred(IEventData data, EventType type)
    {
        if (type != EventType.RENDER_TEXT) return;

        var info = (TextRenderInfo)data;
        var pt = info.GetBaseline().GetStartPoint();

        Chunks.Add(new Chunk
        {
            Text = info.GetText(),
            X = pt.Get(Vector.I1),
            Y = pt.Get(Vector.I2)
        });
    }

    public ICollection<EventType> GetSupportedEvents() => null;
}
public class Extractor
{
    bool IsAnchorLine(List<Chunk> line)
    {
        bool hasQty = line.Any(c => Regex.IsMatch(c.Text, @"\d+\.\d+"));
        bool hasDate = line.Any(c => Regex.IsMatch(c.Text, @"\d{2}/\d{2}"));
        bool hasMat = line.Any(c => Regex.IsMatch(c.Text, @"A\d+"));

        return hasQty && hasDate && hasMat;
    }
    public DataTable Run(string pdf)
    {
        var chunks = Load(pdf);

        DataTable dt = CreateTable();

        // 🔥 group theo dòng (Y)
        var lines = chunks
            .OrderByDescending(c => c.Y)
            .GroupBy(c => Math.Round(c.Y / 2) * 2)
            .Select(g => g.ToList())
            .ToList();

        foreach (var line in lines)
        {
            // 🔥 lấy theo vùng X (KHÔNG regex)
            string matno = GetText(line, 500, 650);
            string mlm = GetText(line, 650, 750);
            string unit = GetText(line, 750, 800);
            string qty = GetText(line, 800, 860);
            string date = GetText(line, 860, 920);
            string item = GetText(line, 920, 1000);

            // 🔥 điều kiện có dữ liệu thật
            if (!string.IsNullOrWhiteSpace(qty) &&
                !string.IsNullOrWhiteSpace(matno))
            {
                // 🔥 lấy name từ các dòng phía trên
                string name = GetName(lines, line, chunks);

                dt.Rows.Add(
                    item.Trim(),
                    matno.Trim(),
                    mlm.Trim(),
                    name.Trim(),
                    unit.Trim(),
                    qty.Trim(),
                    date.Trim(),
                    ""
                );
            }
        }

        return dt;
    }
    string GetText(List<Chunk> line, float minX, float maxX)
    {
        return string.Join(" ",
            line.Where(c => c.X >= minX && c.X < maxX)
                .OrderBy(c => c.X)
                .Select(c => c.Text));
    }
    string GetName(List<List<Chunk>> lines, List<Chunk> currentLine, List<Chunk> all)
    {
        float y = currentLine.First().Y;

        var nameChunks = all
            .Where(c => c.Y > y && c.Y < y + 100 && c.X < 700)
            .OrderByDescending(c => c.Y)
            .ThenBy(c => c.X)
            .Select(c => c.Text);

        return string.Join(" ", nameChunks);
    }
    bool IsEndLine(string line)
    {
        return Regex.IsMatch(line, @"\d+\.\d+")   // Qty
            && Regex.IsMatch(line, @"\d{2}/\d{2}") // Date
            && Regex.IsMatch(line, @"A\d{6,}");    // MatNo
    }
    object[] Parse(string text)
    {
        var m = Regex.Match(text,
            @"(?<unit>[A-Z]+)\s+(?<qty>\d+\.\d+)\s+(?<date>\d{2}/\d{2})\s*(?<item>\d+)\s+(?<matno>A\d+)\s+(?<mlm>\d+)");

        if (!m.Success) return null;

        string name = text.Replace(m.Value, "").Trim();

        return new object[]
        {
        m.Groups["item"].Value,
        m.Groups["matno"].Value,
        m.Groups["mlm"].Value,
        Clean(name),
        m.Groups["unit"].Value,
        m.Groups["qty"].Value,
        m.Groups["date"].Value,
        ""
        };
    }

    private List<Chunk> Load(string pdf)
    {
        var st = new Strategy();

        using (var doc = new PdfDocument(new PdfReader(pdf)))
        {
            for (int i = 1; i <= doc.GetNumberOfPages(); i++)
            {
                var p = new PdfCanvasProcessor(st);
                p.ProcessPageContent(doc.GetPage(i));
            }
        }

        return st.Chunks;
    }
    private object[] ExtractItem(List<Chunk> chunks, Chunk anchor)
    {
        float y = anchor.Y;

        // 🔥 lấy toàn bộ vùng phía trên anchor (1 item)
        var band = chunks
            .Where(c => c.Y <= y + 2 && c.Y >= y - 120) // vùng 1 item
            .ToList();

        var text = string.Join(" ", band
            .OrderBy(c => c.Y)
            .ThenBy(c => c.X)
            .Select(c => c.Text));

        // 🔥 parse anchor line
        var m = Regex.Match(text,
            @"(?<unit>[A-Z]+)\s+(?<qty>\d+\.\d+)\s+(?<date>\d{2}/\d{2})(?<item>\d+)\s+(?<matno>\S+)\s+(?<mlm>\S+)");

        if (!m.Success) return null;

        string unit = m.Groups["unit"].Value;
        string qty = m.Groups["qty"].Value;
        string date = m.Groups["date"].Value;
        string item = m.Groups["item"].Value;
        string matno = m.Groups["matno"].Value;
        string mlm = m.Groups["mlm"].Value;

        // 🔥 name = phần còn lại
        string name = text.Replace(m.Value, "").Trim();

        return new object[]
        {
        item,
        matno,
        mlm,
        Clean(name),
        unit,
        qty,
        date,
        ""
        };
    }
    private DataTable CreateTable()
    {
        var dt = new DataTable();

        dt.Columns.Add("Item");
        dt.Columns.Add("MatNo");
        dt.Columns.Add("MLM");
        dt.Columns.Add("Name");
        dt.Columns.Add("Unit");
        dt.Columns.Add("Qty");
        dt.Columns.Add("Date");
        dt.Columns.Add("Remark");

        return dt;
    }

    private string Clean(string s)
    {
        return Regex.Replace(s, @"\s+", " ").Trim();
    }
}