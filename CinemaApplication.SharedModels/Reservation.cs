namespace CinemaApplication.SharedModels
{
    public class Reservation
    {
        public int? MovieId { get; set; }
        public string? UserId { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
