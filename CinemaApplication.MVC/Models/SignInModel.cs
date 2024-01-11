using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.MVC.Models;

public class SignInModel
{
    [Required(ErrorMessage = "This field is required")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "This field is required")]
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
}

