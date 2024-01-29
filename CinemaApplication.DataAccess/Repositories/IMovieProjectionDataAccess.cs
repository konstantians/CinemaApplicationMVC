using CinemaApplication.SharedModels;

namespace CinemaApplication.DataAccess.Repositories;

public interface IMovieProjectionDataAccess
{
    Task<int> CreateMovieProjectionAsync(MovieProjection projection);
    Task<bool> CreateMovieProjectionsAsync(List<MovieProjection> projections);
    Task<bool> DeleteMovieProjectionAsync(int id);
    Task<MovieProjection> GetMovieProjectionAsync(int id);
    Task<IEnumerable<MovieProjection>> GetMovieProjectionsAsync();
    Task UpdateMovieProjectionAsync(int id, MovieProjection projection);
}