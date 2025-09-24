using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Interfaces.Services;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Services
{
    public class PdfResourceService : IPdfResourceService
    {
        private readonly IPdfResourceRepository _pdfRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public PdfResourceService(IPdfResourceRepository pdfRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _pdfRepository = pdfRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<PdfResourceResponseDto>> GetAllPdfsAsync()
        {
            var pdfs = await _pdfRepository.GetAllAsync();
            return pdfs.Select(p => new PdfResourceResponseDto
            {
                ResourceId = p.ResourceId,
                Title = p.Title,
                FilePath = p.FilePath,
                ResourceType = p.ResourceType,
                CategoryId = p.CategoryId,
                UploadedById = p.UploadedById,
                UploadedAt = p.UploadedAt,
                IsActive = p.IsActive
            });
        }

        public async Task<PdfResourceResponseDto?> GetPdfByIdAsync(string id)
        {
            var pdf = await _pdfRepository.GetByIdAsync(id);
            if (pdf == null) return null;

            return new PdfResourceResponseDto
            {
                ResourceId = pdf.ResourceId,
                Title = pdf.Title,
                FilePath = pdf.FilePath,
                ResourceType = pdf.ResourceType,
                CategoryId = pdf.CategoryId,
                UploadedById = pdf.UploadedById,
                UploadedAt = pdf.UploadedAt,
                IsActive = pdf.IsActive
            };
        }

        public async Task<PdfResourceResponseDto> AddPdfAsync(PdfResourceRequestDto request)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null) throw new Exception("Invalid category");

            var user = await _userRepository.GetByUserIdAsync(request.UploadedById);
            if (user == null || user.Role?.RoleName.ToLower() != "faculty")
                throw new Exception("Only Faculty can upload PDFs");

            // Generate next ResourceId
            var allResourceIds = (await _pdfRepository.GetAllAsync())
                                 .Select(r => r.ResourceId)
                                 .Where(rid => rid.StartsWith("R"))
                                 .ToList();

            int maxNumber = 0;
            foreach (var rid in allResourceIds)
            {
                if (int.TryParse(rid.Substring(1), out int num))
                {
                    if (num > maxNumber) maxNumber = num;
                }
            }

            int nextNumber = maxNumber + 1;

            var pdf = new PdfResource
            {
                ResourceId = $"R{nextNumber}",
                Title = request.Title,
                FilePath = request.FilePath,
                ResourceType = request.ResourceType ?? "IEEE Paper",
                CategoryId = request.CategoryId,
                UploadedById = request.UploadedById,
                UploadedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _pdfRepository.AddAsync(pdf);
            await _pdfRepository.SaveChangesAsync();

            return new PdfResourceResponseDto
            {
                ResourceId = pdf.ResourceId,
                Title = pdf.Title,
                FilePath = pdf.FilePath,
                ResourceType = pdf.ResourceType,
                CategoryId = pdf.CategoryId,
                UploadedById = pdf.UploadedById,
                UploadedAt = pdf.UploadedAt,
                IsActive = pdf.IsActive
            };
        }


        public async Task<PdfResourceResponseDto> UpdatePdfAsync(string resourceId, PdfResourceRequestDto request)
        {
            var pdf = await _pdfRepository.GetByIdAsync(resourceId);
            if (pdf == null) throw new Exception("PDF not found");

            pdf.Title = request.Title;
            pdf.FilePath = request.FilePath;
            pdf.ResourceType = request.ResourceType ?? pdf.ResourceType;
            pdf.CategoryId = request.CategoryId;
            pdf.UploadedById = request.UploadedById;

            _pdfRepository.Update(pdf);
            await _pdfRepository.SaveChangesAsync();

            return new PdfResourceResponseDto
            {
                ResourceId = pdf.ResourceId,
                Title = pdf.Title,
                FilePath = pdf.FilePath,
                ResourceType = pdf.ResourceType,
                CategoryId = pdf.CategoryId,
                UploadedById = pdf.UploadedById,
                UploadedAt = pdf.UploadedAt,
                IsActive = pdf.IsActive
            };
        }

        public async Task DeletePdfAsync(string resourceId)
        {
            var pdf = await _pdfRepository.GetByIdAsync(resourceId);
            if (pdf == null) throw new Exception("PDF not found");

            _pdfRepository.Delete(pdf);
            await _pdfRepository.SaveChangesAsync();
        }
    }
}
