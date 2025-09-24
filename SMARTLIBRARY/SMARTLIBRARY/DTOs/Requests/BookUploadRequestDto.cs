//using Microsoft.AspNetCore.Http;
//using System.ComponentModel.DataAnnotations;

//namespace SMARTLIBRARY.DTOs.Requests
//{
//    public class BookUploadRequestDto
//    {
//        [Required] public string Title { get; set; } = string.Empty;
//        public string Author { get; set; } = string.Empty;
//        public string ISBN { get; set; } = string.Empty;

//        [Required] public string CategoryId { get; set; } = string.Empty;
//        [Required] public string UploadedById { get; set; } = string.Empty;

//        // Optional PDF file
//        public IFormFile? PdfFile { get; set; }

//        // Cover image required
//        [Required] public IFormFile CoverImage { get; set; } = null!;
//    }
//}
