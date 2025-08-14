using APICodeFirst_Interface.Interfaces;
using APICodeFirst_Interface.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICodeFirst_Interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _bookService;

        public BookController(IBook bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            var createdBook = await _bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.BookId }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.BookId) return BadRequest();

            var updatedBook = await _bookService.UpdateBookAsync(book);
            if (updatedBook == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await _bookService.DeleteBookAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
