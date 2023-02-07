using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
	public class Login
	{

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
