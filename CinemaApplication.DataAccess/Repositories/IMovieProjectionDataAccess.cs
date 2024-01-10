using CinemaApplication.SharedModels;

namespace CinemaApplication.DataAccess.Repositories;

public interface IMovieProjectionDataAccess
{
    Task<int> CreateMovieProjectionAsync(MovieProjection movie);
    Task DeleteMovieProjectionAsync(int id);
    Task<MovieProjection> GetMovieProjectionAsync(int id);
    Task<IEnumerable<MovieProjection>> GetMovieProjectionsAsync();
    Task UpdateMovieProjectionAsync(int id, MovieProjection projection);
}