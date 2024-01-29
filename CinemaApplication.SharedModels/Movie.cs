using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.SharedModels;
public class Movie
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please enter the film's title")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Please enter a description for the film")]
    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Please enter the director's name.")]
    public string? Director { get; set; }

    [Required(ErrorMessage = "Please enter the film's category")]
    public string? Category { get; set; }

    [Required(ErrorMessage = "Please enter the film's release date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ReleaseDate { get; set; }

    [Required(ErrorMessage = "Please enter the film's rating")]
    [Range(0, 5, ErrorMessage = "The rating value can either be 0,1,2,3,4 or 5")]
    public int Rating { get; set; }

    [Required]

    public bool IsAgeRestricted { get; set; }

    [Required]
    [Range(45, 240, ErrorMessage = "Film duration must be between 45 and 240 minutes.")]
    public int FilmDuration { get; set; }
    public List<MovieProjection> Projections { get; set; } = new List<MovieProjection>();
}
