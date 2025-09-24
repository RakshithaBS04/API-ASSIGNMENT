using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;

namespace SMARTLIBRARY.Interfaces.Services
{
    public interface IPdfResourceService
    {
        Task<IEnumerable<PdfResourceResponseDto>> GetAllPdfsAsync();
        Task<PdfResourceResponseDto?> GetPdfByIdAsync(string id);
        Task<PdfResourceResponseDto> AddPdfAsync(PdfResourceRequestDto request);
        Task<PdfResourceResponseDto> UpdatePdfAsync(string resourceId, PdfResourceRequestDto request);
        Task DeletePdfAsync(string resourceId);
    }
}
