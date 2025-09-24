using SMARTLIBRARY.DTOs.Responses;

namespace SMARTLIBRARY.Interfaces.Services
{
    public interface IAccessLogService
    {
        Task<IEnumerable<AccessLogResponseDto>> GetLogsByUserAsync(string userId);
        Task<AccessLogResponseDto> LogAccessAsync(string userId, string? bookId, string? resourceId, string accessType);
    }
}
