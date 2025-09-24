using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Services;

namespace SMARTLIBRARY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccessLogsController : ControllerBase
    {
        private readonly IAccessLogService _accessLogService;

        public AccessLogsController(IAccessLogService accessLogService)
        {
            _accessLogService = accessLogService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetLogsByUser(string userId)
        {
            var logs = await _accessLogService.GetLogsByUserAsync(userId);
            return Ok(logs);
        }

        [HttpPost]
        [Authorize (Roles = "Admin,Librarian")]
        public async Task<IActionResult> LogAccess(string userId, string? bookId, string? resourceId, string accessType)
        {
            var log = await _accessLogService.LogAccessAsync(userId, bookId, resourceId, accessType);
            return Ok(log);
        }
    }
}
