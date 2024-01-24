using CinemaApplication.SharedModels;

namespace CinemaApplication.DataAccess.Repositories;

public interface IMovieDataAccess
{
    Task<int> CreateMovieAsync(Movie movie);
    Task DeleteMovieAsync(int id);
    Task<Movie> GetMovieAsync(int id);
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task UpdateMovieAsync(int id, Movie movie);
}