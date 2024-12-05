using Microsoft.AspNetCore.Mvc;
using SEBNBack.Models;
using SEBNBack.Services.Classes;
using SEBNBack.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SEBNBack.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoleController: ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [Route("AddRole")]
        [HttpPost]
        public async Task<ActionResult<String>> AddRoleAsync([Required] RoleModel role)
        {
            bool res = false;

            res = await _roleService.AddRoleAsync(role);
            if (res)
            {
                return Ok("Role added");
            }
            else
            {
                return BadRequest("Role not added");
            }
        }


        [Route("GetAllRoles")]
        [HttpGet]
        public async Task<ActionResult<List<RoleModel>>> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAll();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Route("GetRoleById/{id}")]
        [HttpGet]
        public async Task<ActionResult<RoleModel>> GetRoleById(int id)
        {
            try
            {
                var role = await _roleService.GetById(id);
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


        [Route("EditRole")]
        [HttpPut]
        public async Task<ActionResult<string>> UpdateRole([Required] RoleModel role)
        {
            bool res = await _roleService.UpdateRoleAsync(role);
            if (res)
            {
                return Ok("Role updated");
            }
            else
            {
                return NotFound("Role not found or not updated");
            }
        }

        [Route("DeleteRole")]
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteRoleAsync([Required] int IdR)
        {
            bool res = await _roleService.DeleteRoleAsync(IdR);
            if (res)
            {
                return Ok("Role deleted");
            }
            else
            {
                return NotFound("Role not found");
            }
        }




    }
}
