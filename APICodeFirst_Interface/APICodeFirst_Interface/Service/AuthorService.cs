using APICodeFirst_Interface.Data;
using APICodeFirst_Interface.Interfaces;
using APICodeFirst_Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace APICodeFirst_Interface.Service
{
    public class AuthorService : IAuthor
    {
        private readonly LibraryContext _context;

        public AuthorService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors
                .Include(a => a.Books) // Include related books
                .ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorId == id);
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author?> UpdateAuthorAsync(Author author)
        {
            var existing = await _context.Authors.FindAsync(author.AuthorId);
            if (existing == null) return null;

            existing.Name = author.Name;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
