using CinemaApplication.SharedModels;

namespace CinemaApplication.DataAccess.Repositories;

public interface IMovieDataAccess
{
    Task<int> CreateMovieAsync(Movie movie);
    Task<bool> DeleteMovieAsync(int id);
    Task<Movie> GetMovieAsync(int id);
    Task<Movie> GetMovieByTitleAsync(string title);
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task<bool> UpdateMovieAsync(int id, Movie movie);
}