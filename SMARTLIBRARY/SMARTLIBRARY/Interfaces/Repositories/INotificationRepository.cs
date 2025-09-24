using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Interfaces.Repositories
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetNotificationsByUserAsync(string userId);
    }
}
