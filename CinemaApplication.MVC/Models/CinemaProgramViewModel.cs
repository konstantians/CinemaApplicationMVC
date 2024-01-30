using CinemaApplication.SharedModels;

namespace CinemaApplication.MVC.Models;

public class CinemaProgramViewModel
{
    public List<BankCard> BankCards { get; set; } = new List<BankCard>();
    public List<Movie> Movies { get; set; } = new List<Movie>();
    public Ticket Ticket { get; set; }
    public string UserId { get; set; }
}
