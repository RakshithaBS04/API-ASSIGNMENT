using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Services;

namespace SMARTLIBRARY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{userId}")]
        [Authorize] // All logged-in users can view their notifications
        public async Task<IActionResult> GetNotifications(string userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserAsync(userId);
            return Ok(notifications);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequestDto request)
        {
            var notification = await _notificationService.SendNotificationAsync(request);
            return Ok(notification);
        }
    }
}
