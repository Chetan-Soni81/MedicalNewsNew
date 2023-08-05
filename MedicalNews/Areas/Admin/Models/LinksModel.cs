using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
    public class LinksModel
    {
        public int ImportantLinkId { get; set; }
        [Required(ErrorMessage = "* Title is required")]
        public string? WebTitle { get; set; }
        [Required(ErrorMessage = "* URL is required")]
        public string? WebUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AdminLoginId { get; set; }
    }
}
