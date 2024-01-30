using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.SharedModels;
public class Ticket
{
    public int Id { get; set; }
    [Required]
    [Range(1,10,ErrorMessage = "The number of tickets for one reservation must be from 1 to 10.")]
    public int NumberOfSeats { get; set; }
    [Required]
    public int ReservationRefundPrice { get; set; }
    [Required]
    public int MovieProjectionId { get; set; }
    public MovieProjection? MovieProjection { get; set; }
    public string? UserId { get; set; }
}
