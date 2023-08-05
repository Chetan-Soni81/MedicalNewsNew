using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
    public class AdsUpdateModel
    {
        public int AdsId { get; set; }
        [Required(ErrorMessage = "* Title is required")]
        public string? Title { get; set; }
        public string? WebUrl { get; set; }
        [Required(ErrorMessage = "* Field is required")]
        public int? Position { get; set; }
        public IFormFile? AdsImage { get; set; }
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "* Show From Date is required")]
        public DateTime? ShowFrom { get; set; }
        [Required(ErrorMessage = "* Show To Date is required")]
        public DateTime? ShowTo { get; set; }
    }
}
