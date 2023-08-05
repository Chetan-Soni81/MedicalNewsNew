using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
    public class MagazineModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "* Title is required.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "* Cover image is required.")]
        public IFormFile? CoverImage { get; set; }
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "* Short Description is required")]
        public string? ShortDescription { get; set; }
        [Required(ErrorMessage = "* Issue is required")]
        public DateTime? IssueDate { get; set; }

    }
}
