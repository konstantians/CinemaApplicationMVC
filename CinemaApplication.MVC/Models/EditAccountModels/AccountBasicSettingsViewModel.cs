using CinemaApplication.SharedModels;
using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.MVC.Models.EditAccountModels;

public class AccountBasicSettingsViewModel
{
    [Required(ErrorMessage = "This field is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "This field is required")]
    public string? AccountType { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [Phone]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    public List<BankCard> BankCards { get; set; } = new List<BankCard>();
    public List<Ticket> Tickets { get; set; } = new List<Ticket>();

}
