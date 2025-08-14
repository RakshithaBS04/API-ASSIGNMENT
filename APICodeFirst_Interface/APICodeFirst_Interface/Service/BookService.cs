using APICodeFirst_Interface.Data;
using APICodeFirst_Interface.Interfaces;
using APICodeFirst_Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace APICodeFirst_Interface.Service
{
    public class BookService : IBook
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author) // Include related author
                .ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> UpdateBookAsync(Book book)
        {
            var existing = await _context.Books.FindAsync(book.BookId);
            if (existing == null) return null;

            existing.Title = book.Title;
            existing.AuthorId = book.AuthorId;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
