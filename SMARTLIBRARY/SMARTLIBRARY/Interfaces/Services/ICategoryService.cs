using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;

namespace SMARTLIBRARY.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto?> GetCategoryByIdAsync(string id);
        Task<CategoryResponseDto> AddCategoryAsync(CategoryRequestDto request);
        Task<CategoryResponseDto> UpdateCategoryAsync(string categoryId, CategoryRequestDto request);
        Task DeleteCategoryAsync(string categoryId);
    }
}
