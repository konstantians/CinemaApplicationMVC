using CinemaApplication.MVC.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.MVC.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [ComparePassword("Password", ErrorMessage = "The passwords do not match.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [ComparePassword("Password", ErrorMessage = "The passwords do not match.")]
        [Display(Name = "Repeat Password")]
        public string? RepeatPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? AccountType { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        public bool IsEmailConfirmed { get; set; }
    }
}
