using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Interfaces.Services;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;

        public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<NotificationResponseDto>> GetNotificationsByUserAsync(string userId)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserAsync(userId);
            return notifications.Select(n => new NotificationResponseDto
            {
                NotificationId = n.NotificationId,
                Title = n.Title,
                Message = n.Message,
                RecipientId = n.RecipientId,
                CreatedAt = n.CreatedAt,
                IsRead = n.IsRead
            });
        }

        public async Task<NotificationResponseDto> SendNotificationAsync(NotificationRequestDto request)
        {
            var recipient = await _userRepository.GetByUserIdAsync(request.RecipientId);
            if (recipient == null) throw new Exception("Recipient not found");

            var notification = new Notification
            {
                Title = request.Title,
                Message = request.Message,
                RecipientId = request.RecipientId,
                CreatedAt = new DateTime(2025, 9, 22),
                IsRead = false
            };

            await _notificationRepository.AddAsync(notification);
            await _notificationRepository.SaveChangesAsync();

            return new NotificationResponseDto
            {
                NotificationId = notification.NotificationId,
                Title = notification.Title,
                Message = notification.Message,
                RecipientId = notification.RecipientId,
                CreatedAt = notification.CreatedAt,
                IsRead = notification.IsRead
            };
        }
    }
}
