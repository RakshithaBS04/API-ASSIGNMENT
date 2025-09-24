using Microsoft.EntityFrameworkCore;
using SMARTLIBRARY.Data;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(SmartLibraryDbContext context) : base(context) { }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserAsync(string userId)
        {
            return await _dbSet.Where(n => n.RecipientId == userId)
                               .ToListAsync();
        }
    }
}
