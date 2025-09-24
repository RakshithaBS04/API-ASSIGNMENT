using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Interfaces.Repositories
{
    public interface IPdfResourceRepository : IGenericRepository<PdfResource>
    {
        Task<IEnumerable<PdfResource>> GetPdfsByCategoryAsync(string categoryId);
        Task<IEnumerable<PdfResource>> GetPdfsByUserAsync(string userId);
    }
}
