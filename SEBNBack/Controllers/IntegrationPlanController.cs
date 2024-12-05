using Microsoft.AspNetCore.Mvc;
using SEBNBack.Models;
using SEBNBack.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEBNBack.Services.Classes;
using Microsoft.EntityFrameworkCore;
using SebnLibrary.ModelEF;
using OfficeOpenXml;


namespace SEBNBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationPlanController : ControllerBase
    {
        private readonly IIntegrationPlanService _integrationPlanService;

        private readonly SEBNDbLibDbContext _context;

        public IntegrationPlanController(IIntegrationPlanService integrationPlanService, SEBNDbLibDbContext context )
        {
            _integrationPlanService = integrationPlanService;
            _context = context;
        }

        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel([FromForm] int IdIp, [FromForm] string NameIp, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (string.IsNullOrEmpty(NameIp))
            {
                return BadRequest("NameIp is required.");
            }

            try
            {
                // Check if an IntegrationPlan with the given IdIp exists
                var existingPlan = await _context.IntegrationPlans.FindAsync(IdIp);
                if (existingPlan == null)
                {
                    return NotFound($"IntegrationPlan with IdIp {IdIp} not found.");
                }

                // Read the file data
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileData = memoryStream.ToArray();

                    existingPlan.NameIp = NameIp;
                    existingPlan.FileData = fileData;

                    _context.IntegrationPlans.Update(existingPlan);
                    await _context.SaveChangesAsync();

                    return Ok("File uploaded successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.Error.WriteLine($"Exception occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetFile/{idIp}")]
        public async Task<IActionResult> GetFile(int idIp)
        {
            // Retrieve the IntegrationPlan entity
            var plan = await _context.IntegrationPlans
                .Where(p => p.IdIp == idIp)
                .SingleOrDefaultAsync();

            if (plan == null || plan.FileData == null)
            {
                return NotFound("File not found.");
            }

            // Return the file as a downloadable file
            var fileContent = plan.FileData;
            var fileName = $"file_{idIp}.xlsx"; // Customize the file name as needed
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(fileContent, contentType, fileName);
        }


        //[HttpGet("GetFile/{idIp}")]
        //public async Task<IActionResult> GetFile(int idIp)
        //{
        //    // Retrieve the IntegrationPlan entity
        //    var plan = await _context.IntegrationPlans
        //        .Where(p => p.IdIp == idIp)
        //        .SingleOrDefaultAsync();

        //    if (plan == null || plan.FileData == null)
        //    {
        //        return NotFound("File not found.");
        //    }

        //    // Return the file as a downloadable file
        //    var fileContent = plan.FileData;
        //    var fileName = $"file_{idIp}.xlsx"; // Customize the file name as needed
        //    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //    return File(fileContent, contentType, fileName);
        //}


        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadExcel(string name, int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var fileData = stream.ToArray();

                // Validate file data length
                if (fileData.Length == 0)
                {
                    return BadRequest("Uploaded file is empty.");
                }

                var excelData = _integrationPlanService.ProcessExcel(name, id, fileData);

                // Add logging to check if fileData is present
                Console.WriteLine($"File data length in UploadExcel: {fileData.Length}");

                await _integrationPlanService.SaveExcelDataAsync(excelData);
            }

            return Ok("File uploaded and processed successfully.");
        }


        [Route("AddIntegrationPlan")]
        [HttpPost]
        public async Task<ActionResult<string>> AddIntegrationPlanAsync([Required] IntegrationPlanModel integrationPlan)
        {
            bool result = await _integrationPlanService.AddIPAsync(integrationPlan);
            if (result)
            {
                return Ok("Integration plan added");
            }
            else
            {
                return BadRequest("Integration plan not added");
            }
        }

        [Route("DeleteIntegrationPlan/{id}")]
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteIntegrationPlanAsync([Required] int id)
        {
            bool result = await _integrationPlanService.DeleteIPAsync(id);
            if (result)
            {
                return Ok("Integration plan deleted");
            }
            else
            {
                return NotFound("Integration plan not found");
            }
        }



        [Route("EditIntegrationPlan")]
        [HttpPut]
        public async Task<ActionResult<string>> EditIntegrationPlanAsync([Required] IntegrationPlanModel integrationPlan)
        {
            bool res = await _integrationPlanService.UpdateIPAsync(integrationPlan);
            if (res)
            {
                return Ok("Integration plan updated");
            }
            else
            {
                return NotFound("Integration plan not found or not updated");
            }
        }

        [Route("GetAllIntegrationPlans")]
        [HttpGet]
        public async Task<ActionResult<ICollection<IntegrationPlanModel>>> GetAllIntegrationPlansAsync()
        {
            var integrationPlans = await _integrationPlanService.GetAll();
            if (integrationPlans != null && integrationPlans.Count > 0)
            {
                return Ok(integrationPlans);
            }
            else
            {
                return NotFound("No integration plans found");
            }
        }

        [Route("GetIntegrationPlan/{id}")]
        [HttpGet]
        public async Task<ActionResult<IntegrationPlanModel>> GetIntegrationPlanByIdAsync([Required] int id)
        {
            var integrationPlan = await _integrationPlanService.GetById(id);
            if (integrationPlan != null)
            {
                return Ok(integrationPlan);
            }
            else
            {
                return NotFound("Integration plan not found");
            }
        }


        [HttpGet("GetExcelFile/{userId}")]
        public async Task<IActionResult> GetExcelFile(int userId)
        {
            var integrationPlan = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.IdIpNavigation)
                .Select(u => u.IdIpNavigation)
                .FirstOrDefaultAsync();

            if (integrationPlan == null || integrationPlan.FileData == null)
            {
                return NotFound();
            }

            return File(integrationPlan.FileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


        [HttpGet("user/{mat}/file")]
        public async Task<IActionResult> GetExcelFileByUserMat(int mat)
        {
            try
            {
                var fileData = await _integrationPlanService.GetExcelFileDataByUserMat(mat);

                if (fileData == null)
                {
                    return NotFound($"No IntegrationPlan file found for user with mat: {mat}");
                }

                // Return the file as a downloadable response
                return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "IntegrationPlan.xlsx");
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error in GetExcelFileByUserMat in Controller: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



    }
}
