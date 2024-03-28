using DocSumRepository;
using common.model;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Reflection.PortableExecutable;

namespace DocSumServices
{
    public class DocSumService : IDocSumService
    {
        private readonly IDocSumRepo _docSumRepo;

        public DocSumService(IDocSumRepo docSumRepo)
        {
            _docSumRepo = docSumRepo;
        }

        public string ProcessDocument(byte[] documentData, string fileName)
        {
            // Here, you can implement logic to summarize the document
            // For simplicity, we'll just save the document as it is
            var filePath = _docSumRepo.SaveDocument(documentData, fileName);

            // Parse the document
            ParseDocument(filePath);

            return filePath;
        }

        public List<string> ParseDocument(string filePath)
        {
            // Initialize PDF document parser
            PdfReader pdfReader = new PdfReader(filePath);
            PdfDocument pdfDocument = new PdfDocument(pdfReader);
            List<string> pages = new List<string>();
            // Initialize text extraction strategy
            LocationTextExtractionStrategy extractionStrategy = new LocationTextExtractionStrategy();

            // Extract text from each page
            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
            {
                // Get page content
                PdfPage page = pdfDocument.GetPage(i);
                PdfCanvasProcessor parser = new PdfCanvasProcessor(extractionStrategy);
                parser.ProcessPageContent(page);

                // Get extracted text
                string pageText = extractionStrategy.GetResultantText();
                pages.Add(pageText);
                // Process extracted text as needed
                // For example, you can save it to a database, perform further analysis, etc.
                
            }

            // Close PDF document
            pdfDocument.Close();
            return pages;
        }
    }
}
