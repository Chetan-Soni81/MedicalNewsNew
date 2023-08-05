using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
	public class ArticleFilter
	{
		[Required]
		public DateOnly? FromDate { get; set; }
		[Required]
		public DateOnly? ToDate { get; set;}

		public int? CategoryID { get; set; }
		public int? SubCategoryID { get; set;}
	}
}
