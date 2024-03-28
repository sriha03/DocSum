using DocSumRepository;
using common.model;

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

            /*if (filePath != null) 
            { 
                _docSumRepo.ParseDocument(filePath);
            }*/
            return filePath;
        }
    }
}
