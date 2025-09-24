using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByUserIdAsync(string userId);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName);

        // New method to get all users with Role included
        Task<IEnumerable<User>> GetAllUsersWithRolesAsync();
    }
}
