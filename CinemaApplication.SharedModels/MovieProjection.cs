namespace CinemaApplication.SharedModels
{
    public class MovieProjection
    {
        public int Id { get; set; }
        public int SeatsLeft { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }        
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public int CinemaRoomId { get; set; }
        public CinemaRoom? CinemaRoom { get; set; }
    }
}
