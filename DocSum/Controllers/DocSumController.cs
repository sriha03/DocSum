using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DocSumServices;

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
        public ActionResult<string> UploadDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");
            var filePath = "";
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();
                var fileName = file.FileName;

                filePath=_docSumService.ProcessDocument(fileBytes, fileName);
                
            }

            return Ok(_docSumService.ParseDocument(filePath));
        }
    }
}
