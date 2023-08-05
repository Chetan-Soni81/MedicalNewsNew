using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
    public class InterviewCategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage= "* S/no is Required")]
        public int? Sno { get; set; }
        [Required(ErrorMessage = "* Interview category is required")]
        public string? InterviewCategory { get; set; }
    }
}
