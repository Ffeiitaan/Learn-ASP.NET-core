using Microsoft.AspNetCore.Mvc;
using AuthTestProject.Services;
using AuthTestProject.Entities;
using AuthTestProject.Models;

namespace AuthTestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController(IPermissionService permissionService) : ControllerBase
    {
        [HttpPost("Add-Permission")]
        public async Task<ActionResult<PermissionResponseDto>> AddPermission(PermissionDto request)
        {
            var permission = await permissionService.AddPermission(request);

            if(permission is null)
                return BadRequest("OOPS!! Invalide");

            return Ok(permission);
        }
    }
}