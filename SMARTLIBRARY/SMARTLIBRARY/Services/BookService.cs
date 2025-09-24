using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Interfaces.Services;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<BookResponseDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Select(b => new BookResponseDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                FilePath = b.FilePath,
                ImageUrl = b.ImageUrl,
                CategoryId = b.CategoryId,
                UploadedById = b.UploadedById,
                UploadedAt = b.UploadedAt,
                IsActive = b.IsActive
            });
        }

        public async Task<BookResponseDto?> GetBookByIdAsync(string id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            return new BookResponseDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                FilePath = book.FilePath,
                ImageUrl = book.ImageUrl,
                CategoryId = book.CategoryId,
                UploadedById = book.UploadedById,
                UploadedAt = book.UploadedAt,
                IsActive = book.IsActive
            };
        }

        public async Task<BookResponseDto> AddBookAsync(BookRequestDto request)
        {
            // Validate category
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null) throw new Exception("Invalid category");

            // Validate user
            var user = await _userRepository.GetByUserIdAsync(request.UploadedById);
            if (user == null) throw new Exception("Invalid user");

            // Get last BookId from repository
            var lastBookId = await _bookRepository.GetLastBookIdAsync();
            int nextId = 1;

            if (!string.IsNullOrEmpty(lastBookId))
            {
                // Extract numeric part and increment
                nextId = int.Parse(lastBookId.Substring(1)) + 1;
            }

            // Create new book
            var book = new Book
            {
                BookId = "B" + nextId,  // B1, B2, B3, ...
                Title = request.Title,
                Author = request.Author,
                ISBN = request.ISBN,
                FilePath = request.FilePath,
                ImageUrl = request.ImageUrl ?? "/Images/books/defaultbook.jpeg",
                CategoryId = request.CategoryId,
                UploadedById = request.UploadedById,
                UploadedAt = new DateTime(2025, 9, 22), // static date as requested
                IsActive = true
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            // Return response DTO
            return new BookResponseDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                FilePath = book.FilePath,
                ImageUrl = book.ImageUrl,
                CategoryId = book.CategoryId,
                UploadedById = book.UploadedById,
                UploadedAt = book.UploadedAt,
                IsActive = book.IsActive
            };
        }



        public async Task<BookResponseDto> UpdateBookAsync(string bookId, BookRequestDto request)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null) throw new Exception("Book not found");

            book.Title = request.Title;
            book.Author = request.Author;
            book.ISBN = request.ISBN;
            book.FilePath = request.FilePath;
            book.ImageUrl = request.ImageUrl ?? book.ImageUrl;
            book.CategoryId = request.CategoryId;
            book.UploadedById = request.UploadedById;

            _bookRepository.Update(book);
            await _bookRepository.SaveChangesAsync();

            return new BookResponseDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                FilePath = book.FilePath,
                ImageUrl = book.ImageUrl,
                CategoryId = book.CategoryId,
                UploadedById = book.UploadedById,
                UploadedAt = book.UploadedAt,
                IsActive = book.IsActive
            };
        }

        public async Task DeleteBookAsync(string bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null) throw new Exception("Book not found");

            _bookRepository.Delete(book);
            await _bookRepository.SaveChangesAsync();
        }
    }
}
