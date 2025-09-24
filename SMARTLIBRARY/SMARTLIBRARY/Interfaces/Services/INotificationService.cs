using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;

namespace SMARTLIBRARY.Interfaces.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationResponseDto>> GetNotificationsByUserAsync(string userId);
        Task<NotificationResponseDto> SendNotificationAsync(NotificationRequestDto request);
    }
}
