using Microsoft.EntityFrameworkCore;
using SMARTLIBRARY.Data;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Repositories
{
    public class CategoryRepository : GenericRepository<ResourceCategory>, ICategoryRepository
    {
        public CategoryRepository(SmartLibraryDbContext context) : base(context) { }

        public async Task<ResourceCategory?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
