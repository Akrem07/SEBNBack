using Microsoft.AspNetCore.Mvc;
using SEBNBack.Models;
using SEBNBack.Services.Classes;
using SEBNBack.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEBNBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpEvaController : ControllerBase
    {
        private readonly IEmpEvaService _empEvaService;

        public EmpEvaController(IEmpEvaService empEvaService)
        {
            _empEvaService = empEvaService;
        }

        [HttpPost("Submit")]
        public async Task<ActionResult<string>> SubmitEvaluation([FromBody] EmpEvaModel empEva)
        {
            if (empEva == null)
            {
                return BadRequest("Employee Evaluation model is null.");
            }

            try
            {
                bool res = await _empEvaService.AddEvaAsync(empEva);
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
        public async Task<ActionResult<List<EmpEvaModel>>> GetAllEvaluations()
        {
            try
            {
                var evaluations = await _empEvaService.GetAll();
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
                bool res = await _empEvaService.DeleteEvaluationAsync(id);
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
        public async Task<ActionResult<EmpEvaModel>> GetEvalByMat(int mat)
        {
            try
            {
                var empEval = await _empEvaService.GetBymat(mat);
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
