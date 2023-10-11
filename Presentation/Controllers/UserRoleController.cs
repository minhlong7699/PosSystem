using Contract.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class UserRoleController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UserRoleController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _service.UserRoleService.GetAllUserRoleAsync(trackChanges: false);
            return Ok(roles);
        }
        [HttpGet("{userRoleId:guid}")]
        public async Task<IActionResult> GetRole(Guid userRoleId)
        {
            var role = await _service.UserRoleService.GetUserRoleAsync(userRoleId, trackChanges: false);
            return Ok(role);
        }
    }
}
