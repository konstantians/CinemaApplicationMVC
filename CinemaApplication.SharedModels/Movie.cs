using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.SharedModels;
public class Movie
{
    public int Id { get; set; }
    
    [Required]
    public string? Title { get; set; }
    
    public string? Description { get; set; }

    [Required]
    public string? Director { get; set; }

    [Required]
    public string? Category { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public int Rating { get; set; }

    [Required]
    public bool IsAgeRestricted { get; set; }

    [Required]
    public int FilmDuration { get; set; }
    public List<MovieProjection> Projections { get; set; } = new List<MovieProjection>();
}
