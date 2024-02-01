using CinemaApplication.MVC.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.MVC.Models.EditAccountModels;

public class ChangePasswordModel
{
    public string? OldPassword { get; set; }

    [Required]
    public string? OldPasswordGivenByUser { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [ComparePassword("ConfirmNewPassword", ErrorMessage = "The passwords do not match.")]
    public string? NewPassword { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [ComparePassword("NewPassword", ErrorMessage = "The passwords do not match.")]
    public string? ConfirmNewPassword { get; set; }
}
