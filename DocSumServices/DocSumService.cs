/*using DocSumRepository;
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
*/

using DocSumRepository;
using common.model;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Collections.Generic;
using Azure;
using Azure.AI.TextAnalytics;

namespace DocSumServices
{
    public class DocSumService : IDocSumService
    {
        private readonly IDocSumRepo _docSumRepo;
        private readonly TextAnalyticsClient _textAnalyticsClient;

        public DocSumService(IDocSumRepo docSumRepo, string textAnalyticsEndpoint, string textAnalyticsKey)
        {
            _docSumRepo = docSumRepo;

            // Initialize Text Analytics client
            var credential = new AzureKeyCredential("ef1d66e6592f4cac8079fbcef9c0bd4b");
            var endpoint = new Uri("https://textanalyticssum.cognitiveservices.azure.com/");
            _textAnalyticsClient = new TextAnalyticsClient(endpoint, credential);
        }

        public string ProcessDocument(byte[] documentData, string fileName)
        {
            // Here, you can implement logic to summarize the document
            // For simplicity, we'll just save the document as it is
            var filePath = _docSumRepo.SaveDocument(documentData, fileName);

            // Parse and summarize the document
            var summary = SummarizeDocument(filePath);

            return summary;
        }

        public string SummarizeDocument(string filePath)
        {
            // Parse the document
            List<string> pages = ParseDocument(filePath);

            // Concatenate text from all pages
            string documentText = string.Join(" ", pages);

            /* // Extract key phrases using Text Analytics
             var response = _textAnalyticsClient.ExtractKeyPhrases(documentText);

             // Build summary from key phrases
             List<string> keyPhrases = new List<string>(response.Value);
             string summary = string.Join(", ", keyPhrases);

             return summary;*/
            return documentText;
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
            }

            // Close PDF document
            pdfDocument.Close();
            return pages;
        }
    }
}
