using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.SharedModels;
public class Ticket
{
    public int Id { get; set; }
    [Required]
    public int NumberOfSeats { get; set; }
    [Required]
    public int ReservationRefundPrice { get; set; }
    [Required]
    public string? UserId { get; set; }
    [Required]
    public int MovieProjectionId { get; set; }
    public MovieProjection? MovieProjection { get; set; }
}
