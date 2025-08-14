using APICodeFirst_Interface.Models;

namespace APICodeFirst_Interface.Interfaces
{
    public interface IAuthor
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author> AddAuthorAsync(Author author);
        Task<Author?> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
