using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Services;
using System.IO;

namespace SMARTLIBRARY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfResourcesController : ControllerBase
    {
        private readonly IPdfResourceService _pdfService;

        public PdfResourcesController(IPdfResourceService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpGet]
        [Authorize] // All users can view PDFs
        public async Task<IActionResult> GetAllPdfs()
        {
            var pdfs = await _pdfService.GetAllPdfsAsync();
            return Ok(pdfs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPdfById(string id)
        {
            var pdf = await _pdfService.GetPdfByIdAsync(id);
            if (pdf == null) return NotFound();
            return Ok(pdf);
        }

        // --- Upload (multipart/form-data) ---
        [HttpPost("upload")]
        [Authorize(Roles = "Faculty,Librarian")]
        public async Task<IActionResult> UploadPdf([FromForm] PdfUploadRequestDto request)
        {
            if (request.FilePath == null || request.FilePath.Length == 0)
                return BadRequest("Invalid file.");

            // Validate extension and size
            var ext = Path.GetExtension(request.FilePath.FileName).ToLowerInvariant();
            var allowed = new[] { ".pdf" };
            if (!allowed.Contains(ext))
                return BadRequest("Only PDF files are allowed.");
            // Max 20 MB
            if (request.FilePath.Length > 20 * 1024 * 1024)
                return BadRequest("File too large (max 20 MB).");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "pdfs");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.FilePath.CopyToAsync(stream);
            }

            // Create DB record via existing service
            var dto = new SMARTLIBRARY.DTOs.Requests.PdfResourceRequestDto
            {
                Title = request.Title,
                CategoryId = request.CategoryId,
                UploadedById = request.UploadedById,
                FilePath = $"/uploads/pdfs/{fileName}"
            };

            var created = await _pdfService.AddPdfAsync(dto);
            return Ok(created);
        }

        // --- Download (attachment) ---
        [HttpGet("download/{id}")]
        [Authorize(Roles = "Admin,Librarian,Faculty,Student")]
        public async Task<IActionResult> DownloadPdf(string id)
        {
            var pdf = await _pdfService.GetPdfByIdAsync(id);
            if (pdf == null)
                return NotFound("PDF not found.");

            var relative = pdf.FilePath?.TrimStart('/', '\\') ?? string.Empty;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relative);

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found on server.");

            var provider = new FileExtensionContentTypeProvider();
            provider.TryGetContentType(filePath, out var contentType);
            contentType ??= "application/pdf";

            var fileName = Path.GetFileName(filePath);
            return PhysicalFile(filePath, contentType, fileName);
        }

        // --- Preview (open in browser with range support) ---
        [HttpGet("preview/{id}")]
        [Authorize(Roles = "Admin,Librarian,Faculty,Student")]
        public async Task<IActionResult> PreviewPdf(string id)
        {
            var pdf = await _pdfService.GetPdfByIdAsync(id);
            if (pdf == null)
                return NotFound("PDF not found.");

            var relative = pdf.FilePath?.TrimStart('/', '\\') ?? string.Empty;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relative);

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found on server.");

            var provider = new FileExtensionContentTypeProvider();
            provider.TryGetContentType(filePath, out var contentType);
            contentType ??= "application/pdf";

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return File(stream, contentType, enableRangeProcessing: true);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> UpdatePdf(string id, [FromBody] SMARTLIBRARY.DTOs.Requests.PdfResourceRequestDto request)
        {
            var pdf = await _pdfService.UpdatePdfAsync(id, request);
            return Ok(pdf);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> DeletePdf(string id)
        {
            await _pdfService.DeletePdfAsync(id);
            return NoContent();
        }
    }
}
