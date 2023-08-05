using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
	public class ArticleModel
	{
		public int ArticleId { get; set; }
		[Required(ErrorMessage = "* Category is required")]
		public int? ArticleCategoryId { get; set; }
		public string? ArticleCategoryName { get; set; }
		[Required(ErrorMessage ="* Sub Category is required")]
		public int? ArticleSubCategoryId { get; set; }
		public string? ArticleSubCategoryName { get; set; }
		[Required(ErrorMessage ="* Title is required")]
		public string? ArticleTitle { get; set; }
		[Required(ErrorMessage ="* Short Description is required")]
		public string? ArticleShortDiscription { get; set; }
		public string? WebUrl { get; set; }
		[Required(ErrorMessage ="* Description is required")]
		public string? Description1 { get; set; }
		public string? Description2 { get; set; }
		[Required(ErrorMessage = "* Image is required")]
		public IFormFile? Image1 { get; set; }
		public string? ImageUrl1 { get; set; }
		public IFormFile? Image2 { get; set; }
		public string? ImageUrl2 { get; set; }
		[Required(ErrorMessage ="* Please select a option")]
		public bool? ShowOnHomePage { get; set; }
		[Required(ErrorMessage = "* Please select a option")]
		public bool? ShowOnBanner { get; set; }
		[Required(ErrorMessage = "* Please select a option")]
		public bool? IsBrakeing { get; set; }
		public int AdminLoginId { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
