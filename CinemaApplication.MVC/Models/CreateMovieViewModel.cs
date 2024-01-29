using CinemaApplication.SharedModels;

namespace CinemaApplication.MVC.Models;

public class CreateMovieViewModel
{
    public Movie Movie { get; set; }
    public List<CinemaRoom> CinemaRooms { get; set; } = new List<CinemaRoom>();
}
