using System.ComponentModel.DataAnnotations;

namespace MedicalNews.Areas.Admin.Models
{
    public class AdminLoginModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required(ErrorMessage = "* Username is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "* Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public int RoleId { get; set; }
    }
}