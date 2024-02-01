using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaApplication.SharedModels;

public class AppUser : IdentityUser
{
    [NotMapped]
    public List<BankCard> BankCards { get; set; } = new List<BankCard>();
    [NotMapped]
    public List<Ticket> Tickets { get; set; } = new List<Ticket>();
}
