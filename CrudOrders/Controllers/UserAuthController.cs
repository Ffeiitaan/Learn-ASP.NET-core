using CrudOrders.Models;
using CrudOrders.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudOrders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAuthController : ControllerBase
    {
        private readonly UserAuthService _authService;

        public UserAuthController(UserAuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            }));
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {
            // ПОтім портрібно перемістити валідацію до Middleware
            try
            {
                var result = await _authService.LoginAsync(request);

                return Ok(result);
            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterRequestDto request)
        {
            try
            {
                var result = await _authService.RegisterAsync(request);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}