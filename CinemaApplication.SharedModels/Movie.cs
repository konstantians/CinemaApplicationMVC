namespace CinemaApplication.SharedModels
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Length { get; set; }
        public string? Type { get; set; }
        public string? Summary { get; set; }
        public string? Director { get; set; }
        public List<MovieProjection> Projections { get; set; } = new List<MovieProjection>();
    }
}
