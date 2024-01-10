using CinemaApplication.SharedModels;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.DataAccess.Repositories;

internal class MovieDataAccess : IMovieDataAccess
{
    private readonly AppDbContext _context;
    private readonly IMovieProjectionDataAccess _movieProjectionDataAccess;
    public MovieDataAccess(AppDbContext context, IMovieProjectionDataAccess movieProjectionDataAccess)
    {
        _context = context;
        _movieProjectionDataAccess = movieProjectionDataAccess;
    }

    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {
        try
        {
            return await _context.Movies
                .Include(movie => movie.Projections)
                .ThenInclude(projection => projection.CinemaRoom)
                .Include(movie => movie.Projections)
                .ThenInclude(projection => projection.Reservations)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Movie> GetMovieAsync(int id)
    {
        try
        {
            return await _context.Movies
               .Include(movie => movie.Projections)
               .ThenInclude(projection => projection.CinemaRoom)
               .Include(movie => movie.Projections)
               .ThenInclude(projection => projection.Reservations)
               .FirstOrDefaultAsync(movie => movie.Id == id);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> CreateMovieAsync(Movie movie)
    {
        try
        {
            var result = await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task UpdateMovieAsync(int id, Movie movie)
    {
        try
        {
            Movie foundMovie = await GetMovieAsync(id);
            if (foundMovie is null)
                return;

            foundMovie.Name = movie.Name;
            foundMovie.Length = movie.Length;
            foundMovie.Director = movie.Director;
            foundMovie.Summary = movie.Summary;
            foundMovie.Type = movie.Type;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task DeleteMovieAsync(int id)
    {
        try
        {
            Movie foundMovie = await GetMovieAsync(id);

            foreach (MovieProjection projection in foundMovie.Projections)
            {
                await _movieProjectionDataAccess.DeleteMovieProjectionAsync(projection.Id);
            }
            _context.Movies.Remove(foundMovie);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
