using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Interfaces.Services;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryResponseDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                IsActive = c.IsActive
            });
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };
        }

        public async Task<CategoryResponseDto> AddCategoryAsync(CategoryRequestDto request)
        {
            var existing = await _categoryRepository.GetByNameAsync(request.Name);
            if (existing != null) throw new Exception("Category already exists");

            var category = new ResourceCategory
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive
            };

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };
        }

        public async Task<CategoryResponseDto> UpdateCategoryAsync(string categoryId, CategoryRequestDto request)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null) throw new Exception("Category not found");

            category.Name = request.Name;
            category.Description = request.Description;
            category.IsActive = request.IsActive;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };
        }

        public async Task DeleteCategoryAsync(string categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null) throw new Exception("Category not found");

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
