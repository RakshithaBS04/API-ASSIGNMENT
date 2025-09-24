using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Interfaces.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(string categoryId);
        Task<IEnumerable<Book>> GetBooksByUserAsync(string userId);
        Task<string?> GetLastBookIdAsync();
    }
}
