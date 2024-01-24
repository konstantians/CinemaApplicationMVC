using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.SharedModels;
public class MovieProjection
{
    public int Id { get; set; }
    
    [Required]
    public int SeatsLeft { get; set; }
    
    [Required]
    public DateTime StartingTime { get; set; }
    
    [Required]
    public DateTime EndingTime { get; set; }

    [Required]
    public int CinemaRoomId { get; set; }

    [Required]
    public int MovieId { get; set; }
    
    public Movie? Movie { get; set; }        
    public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    public CinemaRoom? CinemaRoom { get; set; }
}
