using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DocSumServices;
using sun.swing;

namespace DocSumController.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocSumController : ControllerBase
    {
        private readonly IDocSumService _docSumService;

        public DocSumController(IDocSumService docSumService)
        {
            _docSumService = docSumService;
        }

        [HttpPost("uploadpdf")]
        public async Task<List<string>> uploadDocument(IFormFile file)
        {
            //  if (file == null || file.Length == 0)
            // return BadRequest("No file uploaded");
            List<string> summary;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();
                var fileName = file.FileName;

                //  summary =_docSumService.ProcessDocument(fileBytes, fileName);
                List<string> summar = await _docSumService.ProcessDocument(fileBytes, fileName);


                return summar;
            }
        }
    }
}
