using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Interfaces.Services;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Services
{
    public class AccessLogService : IAccessLogService
    {
        private readonly IAccessLogRepository _accessLogRepository;

        public AccessLogService(IAccessLogRepository accessLogRepository)
        {
            _accessLogRepository = accessLogRepository;
        }

        // Get logs by user
        public async Task<IEnumerable<AccessLogResponseDto>> GetLogsByUserAsync(string userId)
        {
            var logs = await _accessLogRepository.GetLogsByUserAsync(userId);
            return logs.Select(l => new AccessLogResponseDto
            {
                LogId = l.LogId,
                UserId = l.UserId,
                BookId = l.BookId,
                ResourceId = l.ResourceId,
                AccessedAt = l.AccessedAt,
                AccessType = l.AccessType
            });
        }

        // Log access to book or resource
        public async Task<AccessLogResponseDto> LogAccessAsync(string userId, string? bookId, string? resourceId, string accessType)
        {
            var log = new ResourceAccessLog
            {
                UserId = userId,
                BookId = bookId,
                ResourceId = resourceId,
                AccessType = accessType,
                AccessedAt = new DateTime(2025, 9, 22) // STATIC DATE
            };

            await _accessLogRepository.AddAsync(log);
            await _accessLogRepository.SaveChangesAsync();

            return new AccessLogResponseDto
            {
                LogId = log.LogId,
                UserId = log.UserId,
                BookId = log.BookId,
                ResourceId = log.ResourceId,
                AccessType = log.AccessType,
                AccessedAt = log.AccessedAt
            };
        }
    }
}
