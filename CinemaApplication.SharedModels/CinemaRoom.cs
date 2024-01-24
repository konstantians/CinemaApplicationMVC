using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.SharedModels;
public class CinemaRoom
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public int AvailableSeats { get; set; }
    [Required]
    public bool Supports3D { get; set; }
    public List<MovieProjection> Projections { get; set; } = new List<MovieProjection>();
}
