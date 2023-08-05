using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
    public class ArticleSubCategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "* S/no. is required.")]
        public int? Sno { get; set; }
        public string? Category { get; set; } = null;
        [Required(ErrorMessage = "* Field is required")]
        public bool? ShowOnHomePage { get; set; }
        [Required(ErrorMessage = "* Field is required")]
        public bool? ShowOnMainMenu { get; set; }
        public int? ArticleCategoryID { get; set; }
        [Required(ErrorMessage = "* Category is required")]
        public string? ArticleSubCategoryName { get; set; }
    }
}
