using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEBNBack.Models;
using SEBNBack.Services.Classes;
using SEBNBack.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SEBNBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService;
        }

        [Route("AddDepartment")]
        [HttpPost]
        public async Task<ActionResult<string>> AddDepartmentAsync([Required] DepartmentModel department)
        {
            bool res = await _departmentService.AddDepAsync(department);
            if (res)
            {
                return Ok("Department added");
            }
            else
            {
                return BadRequest("Department not added");
            }
        }

        [Route("DeleteDepartment")]
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteDepartmentAsync([Required] int IdDep)
        {
            bool res = await _departmentService.DeleteDepAsync(IdDep);
            if (res)
            {
                return Ok("Department deleted");
            }
            else
            {
                return NotFound("Department not found");
            }
        }

        [Route("EditDepartment")]
        [HttpPut]
        public async Task<ActionResult<string>> EditDepartmentAsync([Required] DepartmentModel department)
        {
            bool res = await _departmentService.EditDepAsync(department);
            if (res)
            {
                return Ok("Department updated");
            }
            else
            {
                return NotFound("Department not found or not updated");
            }
        }

        [Route("GetAllDepartments")]
        [HttpGet]
        public async Task<ActionResult<ICollection<DepartmentModel>>> GetAllDepartmentsAsync()
        {
            var deps = await _departmentService.GetAll();
            if (deps != null && deps.Count > 0)
            {
                return Ok(deps);
            }
            else
            {
                return NotFound("No Departments found");
            }
        }


        [Route("GetDepartmentByName/{NameDep}")]
        [HttpGet]
        //public async Task<ActionResult<UserModel>> GetUserByNameAsync(string NameDep)
        //{
        //    try
        //    {
        //        var depModel = await _departmentService.GetDepByName(NameDep);
        //        return Ok(depModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
        public async Task<ActionResult<DepartmentModel>> GetUserByNameAsync(string NameDep)
        {
            try
            {
                var depModel = await _departmentService.GetDepByName(NameDep);
                return Ok(depModel);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("GetDepartmentById/{id}")]
        [HttpGet]
        public async Task<ActionResult<DepartmentModel>> GetRoleById(int id)
        {
            try
            {
                var role = await _departmentService.GetById(id);
                if (role == null)
                {
                    return NotFound();
                }

                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
