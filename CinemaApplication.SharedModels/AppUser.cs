using Microsoft.AspNetCore.Identity;

namespace CinemaApplication.SharedModels;

public class AppUser : IdentityUser
{
    public List<BankCard> BankCards { get; set; } = new List<BankCard>();
    public List<Ticket> Tickets { get; set; } = new List<Ticket>();
}
