using APICodeFirst_Interface.Models;

namespace APICodeFirst_Interface.Interfaces
{
    public interface IBook
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task<Book?> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}
