namespace CinemaApplication.SharedModels
{
    public class Reservation
    {
        public int Id { get; set; }
        public int NumberOfSeats { get; set; }
        public int ReservationRefoundPrice { get; set; }
        public int MovieProjectionId { get; set; }
        public MovieProjection MovieProjection { get; set; }
        public string? UserId { get; set; }
    }
}
