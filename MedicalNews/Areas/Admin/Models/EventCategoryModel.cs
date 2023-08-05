using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
    public class EventCategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "* Category is required")]
        public string? Category { get; set; } = null;
        [Required(ErrorMessage = "* Field is required")]
        public bool? ShowOnHomePage { get; set; }
        [Required(ErrorMessage = "* Field is required")]
        public bool? ShowOnMainMenu { get; set; }
    }
}
