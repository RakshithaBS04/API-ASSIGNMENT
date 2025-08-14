using APICodeFirst_Interface.Interfaces;
using APICodeFirst_Interface.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICodeFirst_Interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthor _authorService;

        public AuthorController(IAuthor authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            var createdAuthor = await _authorService.AddAuthorAsync(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.AuthorId }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            if (id != author.AuthorId) return BadRequest();

            var updatedAuthor = await _authorService.UpdateAuthorAsync(author);
            if (updatedAuthor == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleted = await _authorService.DeleteAuthorAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
