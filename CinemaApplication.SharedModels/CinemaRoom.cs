namespace CinemaApplication.SharedModels
{
    public class CinemaRoom
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AvailableSeats { get; set; }
        public bool Supports3D { get; set; }
        public List<MovieProjection> Projections { get; set; } = new List<MovieProjection>();
    }
}
