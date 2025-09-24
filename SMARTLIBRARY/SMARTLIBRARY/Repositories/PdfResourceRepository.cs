using Microsoft.EntityFrameworkCore;
using SMARTLIBRARY.Data;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Repositories
{
    public class PdfResourceRepository : GenericRepository<PdfResource>, IPdfResourceRepository
    {
        public PdfResourceRepository(SmartLibraryDbContext context) : base(context) { }

        public async Task<IEnumerable<PdfResource>> GetPdfsByCategoryAsync(string categoryId)
        {
            return await _dbSet.Where(p => p.CategoryId == categoryId)
                               .ToListAsync();
        }

        public async Task<IEnumerable<PdfResource>> GetPdfsByUserAsync(string userId)
        {
            return await _dbSet.Where(p => p.UploadedById == userId)
                               .ToListAsync();
        }
    }
}
