using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Geom;
using System.Text;
using System.Collections.Generic;

namespace Julian_Server
{
    public class TabExtractionStrategy : IEventListener
    {
        private StringBuilder result = new StringBuilder();
        private float lastX = 0;

        public void EventOccurred(IEventData data, EventType type)
        {
            if (type != EventType.RENDER_TEXT) return;

            var renderInfo = (TextRenderInfo)data;
            var text = renderInfo.GetText();

            Vector start = renderInfo.GetBaseline().GetStartPoint();
            float currentX = start.Get(Vector.I1);

            // nếu khoảng cách lớn => coi như tab
            if (currentX - lastX > 20) // chỉnh số này tùy PDF
            {
                result.Append("\t");
            }

            result.Append(text);
            lastX = currentX;
        }

        public string GetResult() => result.ToString();

        public ICollection<EventType> GetSupportedEvents() => null;
    }
}
