using Microsoft.AspNetCore.Mvc;
using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Services;

namespace SMARTLIBRARY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            if (result == null)
                return Unauthorized("Invalid credentials");
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto request)
        {
            var user = await _authService.RegisterAsync(request);
            return Ok(user);
        }
    }
}
