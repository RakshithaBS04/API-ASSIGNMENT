using Microsoft.EntityFrameworkCore;
using SMARTLIBRARY.Data;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(SmartLibraryDbContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(string categoryId)
        {
            return await _dbSet.Where(b => b.CategoryId == categoryId)
                               .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByUserAsync(string userId)
        {
            return await _dbSet.Where(b => b.UploadedById == userId)
                               .ToListAsync();
        }
        public async Task<string?> GetLastBookIdAsync()
        {
            // Fetch all books to memory first to safely use TryParse
            var allBooks = await _dbSet.ToListAsync();

            // Filter only BookIds that start with 'B' and have a numeric part
            var lastBook = allBooks
                .Where(b => !string.IsNullOrEmpty(b.BookId) &&
                            b.BookId.StartsWith("B") &&
                            int.TryParse(b.BookId.Substring(1), out _))
                .OrderByDescending(b => int.Parse(b.BookId.Substring(1)))
                .FirstOrDefault();

            return lastBook?.BookId;
        }


    }
}
