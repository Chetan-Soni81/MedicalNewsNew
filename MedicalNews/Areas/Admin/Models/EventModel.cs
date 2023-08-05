using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
	public class EventModel
	{
		[Required(ErrorMessage ="* Field is required")]
		public int? EventCategoryId { get; set; }
		public string? EventCategoryName { get; set; }
		public bool? ShowOnMenu { get; set; }
		[Required(ErrorMessage ="* Field is required")]
		public bool? ShowOnHomePage { get; set; }
		public DateTime? CreatedDate { get; set; }
		public int? AdminLoginId { get; set; }

		public int? EventId { get; set; }
		[Required(ErrorMessage = "* Event Title is required")]
		public string? EventTitle { get; set; }
		public string? ShortDescription { get; set; }
		[Required(ErrorMessage = "* Cover Image is required")]
		public IFormFile? CoverImage { get; set; }
		[Required(ErrorMessage ="* Event Date is required")]
		public DateTime? EventDate { get; set; }
		public int EventDetailId { get; set; }
		public string? ImageUrl { get; set; }
	}
}
