using CinemaApplication.SharedModels;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.DataAccess.Repositories;

public class MovieDataAccess : IMovieDataAccess
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
                .ThenInclude(projection => projection.Tickets)
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
               .ThenInclude(projection => projection.Tickets)
               .FirstOrDefaultAsync(movie => movie.Id == id);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Movie> GetMovieByTitleAsync(string title)
    {
        try
        {
            return await _context.Movies
               .Include(movie => movie.Projections)
               .ThenInclude(projection => projection.CinemaRoom)
               .Include(movie => movie.Projections)
               .ThenInclude(projection => projection.Tickets)
               .FirstOrDefaultAsync(movie => movie.Title == title);

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
            return -1;
        }
    }

    public async Task<bool> UpdateMovieAsync(int id, Movie movie)
    {
        try
        {
            Movie foundMovie = await GetMovieAsync(id);
            if (foundMovie is null)
                return false;

            foundMovie.Title = movie.Title;
            foundMovie.FilmDuration = movie.FilmDuration;
            foundMovie.Director = movie.Director;
            foundMovie.Description = movie.Description;
            foundMovie.Category = movie.Category;
            foundMovie.ReleaseDate = movie.ReleaseDate;
            foundMovie.Rating = movie.Rating;
            foundMovie.IsAgeRestricted = movie.IsAgeRestricted;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteMovieAsync(int id)
    {
        try
        {
            Movie foundMovie = await GetMovieAsync(id);
            if(foundMovie is null) 
                return false;

            _context.Movies.Remove(foundMovie);

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
