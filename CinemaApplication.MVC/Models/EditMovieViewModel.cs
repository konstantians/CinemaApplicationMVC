using CinemaApplication.SharedModels;

namespace CinemaApplication.MVC.Models;

public class EditMovieViewModel
{
    public Movie Movie { get; set; }
    public MovieProjection Projection { get; set; }
    public List<CinemaRoom> CinemaRooms { get; set; } = new List<CinemaRoom>();
}
