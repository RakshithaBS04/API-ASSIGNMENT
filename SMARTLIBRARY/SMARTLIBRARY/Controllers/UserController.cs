using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Services;

namespace SMARTLIBRARY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("role/{roleName}")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var users = await _userService.GetUsersByRoleAsync(roleName);
            return Ok(users);
        }

        // GET: api/User/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // Create a user (Admin -> Librarian, Librarian -> Student/Faculty)
        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserRequestDto request)
        {
            var currentUserRole = User.Claims
                .FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(currentUserRole))
                return Forbid();

            if (currentUserRole == "Admin" && request.RoleName != "Librarian")
                return BadRequest("Admin can only create Librarians.");
            if (currentUserRole == "Librarian" && request.RoleName != "Student" && request.RoleName != "Faculty")
                return BadRequest("Librarian can only create Students or Faculty.");
            if (currentUserRole != "Admin" && currentUserRole != "Librarian")
                return Forbid();

            var user = await _userService.CreateUserAsync(request);
            return Ok(user);
        }

        // Update user
        [HttpPut("{userId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] RegisterUserRequestDto request)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            user.FullName = request.FullName;
            user.Email = request.Email;
            user.ImageUrl = request.ImageUrl ?? user.ImageUrl;
            user.IsActive = true;

            var updatedUser = await _userService.UpdateUserAsync(userId, user);
            return Ok(updatedUser);
        }

        // Delete user
        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}
