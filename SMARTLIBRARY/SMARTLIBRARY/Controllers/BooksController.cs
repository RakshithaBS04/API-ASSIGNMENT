using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Services;
namespace SMARTLIBRARY.Controllers 
{ 
    [Route("api/[controller]")]
    [ApiController] 
    public class BooksController : ControllerBase { 
        private readonly IBookService _bookService; 
        public BooksController(IBookService bookService) { _bookService = bookService; } 

        [HttpGet]
        [Authorize] // All logged-in users can view
        public async Task<IActionResult> GetAllBooks() 
        { 
            var books = await _bookService.GetAllBooksAsync(); 
            return Ok(books); 
        } 

        [HttpGet("{id}")] 
        [Authorize] 
        public async Task<IActionResult> GetBookById(string id) 
        {
            var book = await _bookService.GetBookByIdAsync(id); 
            if (book == null) 
                return NotFound(); 
            return Ok(book); 
        } 

        [HttpPost] 
        [Authorize(Roles = "Admin,Librarian")] 
        public async Task<IActionResult> AddBook([FromBody] BookRequestDto request) 
        { 
            var book = await _bookService.AddBookAsync(request);
            return Ok(book); 
        } 

        [HttpPut("{id}")] 
        [Authorize(Roles = "Admin,Librarian")] 
        public async Task<IActionResult> UpdateBook(string id, [FromBody] BookRequestDto request) 
        { 
            var book = await _bookService.UpdateBookAsync(id, request); 
            return Ok(book); 
        } 

        [HttpDelete("{id}")] 
        [Authorize(Roles = "Admin,Librarian")] 
        public async Task<IActionResult> DeleteBook(string id) 
        { 
            await _bookService.DeleteBookAsync(id); 
            return NoContent(); 
        }
    } 
}