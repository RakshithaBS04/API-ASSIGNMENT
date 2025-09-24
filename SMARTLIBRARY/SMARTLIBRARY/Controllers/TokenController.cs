//using Microsoft.AspNetCore.Mvc;
//using SMARTLIBRARY.DTOs.Requests;
//using SMARTLIBRARY.DTOs.Responses;
//using SMARTLIBRARY.Interfaces.Services;

//namespace SMARTLIBRARY.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class TokenController : ControllerBase
//    {
//        private readonly IAuthService _authService;

//        public TokenController(IAuthService authService)
//        {
//            _authService = authService;
//        }

//        // Login endpoint to get JWT
//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
//        {
//            var authResponse = await _authService.LoginAsync(request);
//            if (authResponse == null)
//                return Unauthorized(new { message = "Invalid UserId or Password" });

//            return Ok(authResponse);
//        }

//        // Example protected endpoint
//        [HttpGet("protected")]
//        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]
//        public IActionResult Protected()
//        {
//            return Ok(new { message = "This is a protected Admin endpoint!" });
//        }
//    }
//}
