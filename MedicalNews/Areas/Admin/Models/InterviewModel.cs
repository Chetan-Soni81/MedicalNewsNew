using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
    public class InterviewModel
    {
        [Required(ErrorMessage = "* Field is required")]
        public int? InterViewCategoryId { get; set; }
        public string? InterViewCategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AdminLoginId { get; set; }
        public int InterviewId { get; set; }
        [Required(ErrorMessage ="* Interview Title is required")]
        public string? InterviewTitle { get; set; }
        [Required(ErrorMessage ="* Video URL is required")]
        public string? VideoUrl { get; set; }
        public string? InterviewDescription { get; set; }
        [Required(ErrorMessage = "* Field is required")]
        public bool? ShowOnHomePage { get; set; }
    }
}
