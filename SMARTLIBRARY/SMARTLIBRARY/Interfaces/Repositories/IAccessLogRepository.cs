using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Interfaces.Repositories
{
    public interface IAccessLogRepository : IGenericRepository<ResourceAccessLog>
    {
        Task<IEnumerable<ResourceAccessLog>> GetLogsByUserAsync(string userId);
        Task<IEnumerable<ResourceAccessLog>> GetLogsByBookAsync(string bookId);
        Task<IEnumerable<ResourceAccessLog>> GetLogsByResourceAsync(string resourceId);
    }
}
