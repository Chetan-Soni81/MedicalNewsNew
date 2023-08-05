
using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
	public class AdsModel
	{
		public int AdsId { get; set; }
		[Required(ErrorMessage ="* Title is required")]
		public string? Title { get; set; }
		public string? WebUrl { get; set; }
		[Required(ErrorMessage ="* Field is required")]
        public int? Position { get; set; }
		[Required(ErrorMessage = "* Ads Image is required")]
		public IFormFile? AdsImage { get; set; }
		public string? ImageUrl { get; set; }
		[Required(ErrorMessage = "* Show From Date is required")]
		public DateTime? ShowFrom { get; set; }
		[Required(ErrorMessage = "* Show To Date is required")]
		public DateTime? ShowTo { get; set; }
		public DateTime CreatedDate { get; set; }
		public int AdminLoginId { get; set; }
	}
}
