using System.ComponentModel.DataAnnotations; 

namespace MedicalNews.Areas.Admin.Models
{
	public class UserModel
	{
		public int LoginId { get; set; }
		[Required(ErrorMessage = "* Field is required")]
		public int? RoleId { get; set; }
		public string? RoleName { get; set; }
		[Required(ErrorMessage ="* Username is required")]
		public string? Username { get; set; }
		[Required(ErrorMessage ="* Password is required")]
		public string? Password { get; set; }
		[Required(ErrorMessage ="* Name is required")]
		public string? Name { get; set; }
		[Required(ErrorMessage ="* Email is required")]
		[EmailAddress(ErrorMessage ="* Enter a valid Email")]
		public string? EmailId { get; set; }
		[Required(ErrorMessage ="* Mobile number is required")]
		[Phone(ErrorMessage ="* Mobile number is not valid")]
		public string? MobileNumber { get; set; }
		[Required(ErrorMessage ="* Address is required")]
		public string? Address { get; set; }
		public string? Status { get; set; }
		public int AdminLoginId { get; set; }

	}
}
