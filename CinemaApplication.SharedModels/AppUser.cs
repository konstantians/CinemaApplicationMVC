using Microsoft.AspNetCore.Identity;

namespace CinemaApplication.SharedModels;

public class AppUser : IdentityUser
{
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
}
