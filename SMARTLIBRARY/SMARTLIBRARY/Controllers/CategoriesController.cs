using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Services;

namespace SMARTLIBRARY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize] // All logged-in users can view
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDto request)
        {
            var category = await _categoryService.AddCategoryAsync(request);
            return Ok(category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] CategoryRequestDto request)
        {
            var category = await _categoryService.UpdateCategoryAsync(id, request);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
