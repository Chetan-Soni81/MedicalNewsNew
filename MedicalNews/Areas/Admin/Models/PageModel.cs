using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
    public class PageModel
    {
        public int Id { get; set; }
        public int? MagId { get; set; }
        public bool? IsFree { get; set; }
        public string? PdfUrl { get; set; }
        public IFormFile? formFile { get; set; }
        public int? PageNo { get; set; }
    }
}
