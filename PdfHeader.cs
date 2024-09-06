using iText.IO.Font.Constants;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace CodeJam4
{
    internal class PdfHeader
    {
        public static void ConvertTextToPdfWithHeader(string content, string name, string? header, int fontSize)
        {
            PdfWriter writer = new PdfWriter(name + ".pdf");
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document doc = new Document(pdfDoc);

            // Add event handler for header
            pdfDoc.AddEventHandler(PdfDocumentEvent.END_PAGE, new HeaderEventHandler(doc, header, fontSize));

            // Add content to the document
            doc.Add(new Paragraph(content));

            // Close the document
            doc.Close();
        }

        // Event handler to add header on each page
        public class HeaderEventHandler : IEventHandler
        {
            private readonly Document doc;
            public string header;
            public int fontSize;

            public HeaderEventHandler(Document doc, string header, int fontSize)
            {
                this.doc = doc;
                this.header = header;
                this.fontSize = fontSize;
            }

            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                PdfPage page = docEvent.GetPage();
                Rectangle pageSize = page.GetPageSize();

                // Define the font for the header
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                float fontSize = this.fontSize;

                // Define the X and Y positions for the header
                float headerX = pageSize.GetWidth() / 2;
                float headerY = pageSize.GetTop() - 20;

                // Add header text
                Canvas canvas = new Canvas(page, pageSize);
                canvas.SetFont(font)
                      .SetFontSize(fontSize)
                      .ShowTextAligned(header, headerX, headerY, TextAlignment.CENTER)
                      .Close();
            }
        }
    }
}
