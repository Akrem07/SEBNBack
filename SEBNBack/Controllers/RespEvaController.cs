using Microsoft.AspNetCore.Mvc;
using SEBNBack.Models;
using SEBNBack.Services.Interfaces;

using SEBNBack.Services.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEBNBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespEvaController : Controller
    {


        private readonly IRespEvaService _respEvaService;

        public RespEvaController(IRespEvaService respEvaService)
        {
            _respEvaService = respEvaService;
        }

        [HttpPost("Submit")]
        public async Task<ActionResult<string>> SubmitEvaluation([FromBody] RespEvaModel respEva)
        {
            if (respEva == null)
            {
                return BadRequest("Employee Evaluation model is null.");
            }

            try
            {
                bool res = await _respEvaService.AddEvaAsync(respEva);
                if (res)
                {
                    return Ok("Employee evaluation added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add employee evaluation.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllEvaluations")]
        public async Task<ActionResult<List<RespEvaModel>>> GetAllEvaluations()
        {
            try
            {
                var evaluations = await _respEvaService.GetAll();
                return Ok(evaluations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteEvaluation/{id}")]
        public async Task<ActionResult<string>> DeleteEvaluation(int id)
        {
            try
            {
                bool res = await _respEvaService.DeleteEvaluationAsync(id);
                if (res)
                {
                    return Ok("Employee evaluation deleted successfully.");
                }
                else
                {
                    return NotFound("Employee evaluation not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Route("GetEvalByMat/{mat}")]
        [HttpGet]
        public async Task<ActionResult<RespEvaModel>> GetEvalByMat(int mat)
        {
            try
            {
                var empEval = await _respEvaService.GetBymat(mat);
                if (empEval == null)
                {
                    return NotFound();
                }

                return Ok(empEval);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
