using Microsoft.EntityFrameworkCore;
using SMARTLIBRARY.Data;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Repositories
{
    public class AccessLogRepository : GenericRepository<ResourceAccessLog>, IAccessLogRepository
    {
        public AccessLogRepository(SmartLibraryDbContext context) : base(context) { }

        public async Task<IEnumerable<ResourceAccessLog>> GetLogsByUserAsync(string userId)
        {
            return await _dbSet.Where(l => l.UserId == userId)
                               .ToListAsync();
        }

        public async Task<IEnumerable<ResourceAccessLog>> GetLogsByBookAsync(string bookId)
        {
            return await _dbSet.Where(l => l.BookId == bookId)
                               .ToListAsync();
        }

        public async Task<IEnumerable<ResourceAccessLog>> GetLogsByResourceAsync(string resourceId)
        {
            return await _dbSet.Where(l => l.ResourceId == resourceId)
                               .ToListAsync();
        }
    }
}
