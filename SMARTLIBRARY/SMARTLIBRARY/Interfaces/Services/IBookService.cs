using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;

namespace SMARTLIBRARY.Interfaces.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponseDto>> GetAllBooksAsync();
        Task<BookResponseDto?> GetBookByIdAsync(string id);
        Task<BookResponseDto> AddBookAsync(BookRequestDto request);
        Task<BookResponseDto> UpdateBookAsync(string bookId, BookRequestDto request);
        Task DeleteBookAsync(string bookId);
    }
}
