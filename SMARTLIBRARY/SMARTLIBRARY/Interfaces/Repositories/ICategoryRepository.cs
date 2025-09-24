using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Interfaces.Repositories
{
    public interface ICategoryRepository : IGenericRepository<ResourceCategory>
    {
        Task<ResourceCategory?> GetByNameAsync(string name);
    }
}
