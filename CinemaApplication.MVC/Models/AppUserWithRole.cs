using CinemaApplication.SharedModels;

namespace CinemaApplication.MVC.Models;

public class AppUserWithRole
{
    public AppUser AppUser { get; set; }
    public string AppUserRole { get; set; }
}
