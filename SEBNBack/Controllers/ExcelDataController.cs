using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEBNBack.Services.Interfaces;
using SebnLibrary.Repo.Interfaces;
using System.Threading.Tasks;

namespace SEBNBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelDataController : ControllerBase
    {
        private readonly IExcelDataService _excelDataService;
        private readonly IExcelDataRepo _excelDataRepo;

        public ExcelDataController(IExcelDataService excelDataService, IExcelDataRepo excelDataRepo)
        {
            _excelDataService = excelDataService;
            _excelDataRepo = excelDataRepo;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var excelData = _excelDataService.ProcessExcel(stream.ToArray());
                await _excelDataService.SaveExcelDataAsync(excelData);
            }

            return Ok("File uploaded and processed successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetExcelData()
        {
            var data = await _excelDataRepo.FindAllAsync();
            return Ok(data);
        }

        [HttpGet("files")]
        public async Task<IActionResult> GetExcelFiles()
        {
            var files = await _excelDataRepo.FindAllAsync();
            return Ok(files.Select(f => new { f.Id, f.FileName }));
        }

        [HttpGet("files/{id}")]
        public async Task<IActionResult> GetExcelFileById(int id)
        {
            var file = await _excelDataRepo.GetByIdAsync(id);
            if (file == null)
            {
                return NotFound("File not found.");
            }

            return Ok(file.RowData);
        }

    }
}
