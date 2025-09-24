using Microsoft.EntityFrameworkCore;
using SMARTLIBRARY.Data;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SmartLibraryDbContext context) : base(context) { }

        public async Task<User?> GetByUserIdAsync(string userId)
        {
            return await _dbSet.Include(u => u.Role)
                               .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.Include(u => u.Role)
                               .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName)
        {
            return await _dbSet.Include(u => u.Role)
                               .Where(u => u.Role!.RoleName == roleName)
                               .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersWithRolesAsync()
        {
            return await _dbSet.Include(u => u.Role).ToListAsync();
        }
    }
}
