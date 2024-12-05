using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEBNBack.Models;
using SEBNBack.Services.Interfaces;
using SebnLibrary.ModelEF;
using System.ComponentModel.DataAnnotations;
using static Azure.Core.HttpHeader;

namespace SEBNBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FicheFonctionController : ControllerBase
    {
        private readonly IFicheFonctionService _ficheFonctionService;
        private readonly SEBNDbLibDbContext _context;

        public FicheFonctionController(IFicheFonctionService ficheFonctionService, SEBNDbLibDbContext context)
        {
            this._ficheFonctionService = ficheFonctionService;
            _context = context;
        }


        //[Route("AddFicheFonction")]
        //[HttpPost]
        //public async Task<ActionResult<String>> AddFFAsync([Required] FicheFonctionModel ficheFonction)
        //{
        //    bool res = false;

        //    res = await _ficheFonctionService.AddFFAsync(ficheFonction);
        //    if (res)
        //    {
        //        return Ok("FicheFonction added");
        //    }
        //    else
        //    {
        //        return BadRequest("FicheFonction not added");
        //    }
        //}

        [Route("DeleteFicheFonction")]
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteFFAsync([FromQuery, Required] int id)
        {
            bool res = await _ficheFonctionService.DeleteFFAsync(id);
            if (res)
            {
                return Ok("FicheFonction deleted");
            }
            else
            {
                return NotFound("FicheFonction not found");
            }
        }


        [Route("EditFicheFonction")]
        [HttpPut]
        public async Task<ActionResult<string>> UpdateFFAsync([Required] UpdateFF ficheFonction)
        {
            bool res = await _ficheFonctionService.UpdateFFAsync(ficheFonction);
            if (res)
            {
                return Ok("FicheFonction updated");
            }
            else
            {
                return NotFound("FicheFonction not found or not updated");
            }
        }

        [Route("GetAllfichesFonction")]
        [HttpGet]
        public async Task<ActionResult<ICollection<FicheFonctionModel>>> GetAllFFsAsync()
        {
            var ficheFonctions = await _ficheFonctionService.GetAll();
            if (ficheFonctions != null && ficheFonctions.Count > 0)
            {
                return Ok(ficheFonctions);
            }
            else
            {
                return NotFound("No fiche Fonction found");
            }
        }



        //[Route("UploadficheFonction")]
        //[HttpPost]
        //public async Task<IActionResult> UploadPdf(IFormFile fileDate, int? mresp)
        //{
        //    if (fileDate == null || fileDate.Length == 0)
        //    {
        //        return BadRequest("Please upload a valid PDF file.");
        //    }

        //    if (Path.GetExtension(fileDate.FileName).ToLower() != ".pdf")
        //    {
        //        return BadRequest("Only PDF files are allowed.");
        //    }

        //    byte[] fileData;

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        await fileDate.CopyToAsync(memoryStream);
        //        fileData = memoryStream.ToArray();
        //    }

        //    var pdfFile = new FicheFonction
        //    {
        //        NameFf = fileDate.FileName,
        //        ContentType = fileDate.ContentType,
        //        FileData = fileData,
        //        Mresp = mresp // Set the mat of the responsible user
        //    };

        //    _context.FicheFonctions.Add(pdfFile);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { Message = "PDF file uploaded successfully.", FileId = pdfFile.IdFf });
        //}



        [Route("UploadficheFonction")]
        [HttpPost]
        public async Task<IActionResult> UploadPdf(IFormFile fileDate, int? mresp)
        {
            if (fileDate == null || fileDate.Length == 0)
            {
                return BadRequest("Please upload a valid PDF file.");
            }

            if (Path.GetExtension(fileDate.FileName).ToLower() != ".pdf")
            {
                return BadRequest("Only PDF files are allowed.");
            }

            byte[] fileData;

            using (var memoryStream = new MemoryStream())
            {
                await fileDate.CopyToAsync(memoryStream);
                fileData = memoryStream.ToArray();
            }

            var pdfFile = new FicheFonction
            {
                NameFf = fileDate.FileName,
                ContentType = fileDate.ContentType,
                FileData = fileData,
                Mresp = mresp // Set the mat of the responsible user
            };

            _context.FicheFonctions.Add(pdfFile);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "PDF file uploaded successfully.", FileId = pdfFile.IdFf });
        }




        //[Route("GetficheFonction/{idFf}")]
        //[HttpGet]
        //public async Task<IActionResult> GetPdf(int idFf)
        //{
        //    var pdfFile = await _context.FicheFonctions.FindAsync(idFf);

        //    if (pdfFile == null)
        //    {
        //        return NotFound("PDF file not found.");
        //    }

        //    if (pdfFile.FileData == null || pdfFile.FileData.Length == 0)
        //    {
        //        return BadRequest("File data is missing or corrupt.");
        //    }

        //    // Check for null or invalid ContentType and provide a default
        //    var contentType = string.IsNullOrWhiteSpace(pdfFile.ContentType)
        //        ? "application/octet-stream" // Default to binary stream if unknown
        //        : pdfFile.ContentType;

        //    return File(pdfFile.FileData, contentType, pdfFile.NameFf);
        //}


        // ASP.NET Core Controller method
        [HttpGet("GetficheFonction/{idFf}")]
        public async Task<IActionResult> GetPdf(int idFf)
        {
            var pdfFile = await _context.FicheFonctions.FindAsync(idFf);

            if (pdfFile == null)
            {
                return NotFound("PDF file not found.");
            }

            if (pdfFile.FileData == null || pdfFile.FileData.Length == 0)
            {
                return BadRequest("File data is missing or corrupt.");
            }

            var contentType = string.IsNullOrWhiteSpace(pdfFile.ContentType)
                ? "application/pdf" // Ensure this is set to PDF
                : pdfFile.ContentType;

            return File(pdfFile.FileData, contentType, pdfFile.NameFf);
        }



        [Route("UpdateficheFonction/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdatePdf(int id, IFormFile file, [FromForm] int? mresp)
        {
            var existingPdf = await _context.FicheFonctions.FindAsync(id);

            if (existingPdf == null)
            {
                return NotFound("PDF file not found.");
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("A valid PDF file must be provided.");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
            {
                return BadRequest("Only PDF files are allowed.");
            }

            // Update the file content
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                existingPdf.FileData = memoryStream.ToArray();
                existingPdf.ContentType = file.ContentType;
            }


            if (mresp.HasValue)
            {
                existingPdf.Mresp = mresp.Value;
                existingPdf.NameFf = file.FileName;
                existingPdf.ContentType = file.ContentType;

            }

            // Save the changes
            _context.FicheFonctions.Update(existingPdf);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "PDF file updated successfully.", FileId = existingPdf.IdFf });
        }


    }
}
