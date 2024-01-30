using CinemaApplication.SharedModels;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.DataAccess.Repositories;

public class MovieProjectionDataAccess : IMovieProjectionDataAccess
{
    private readonly AppDbContext _context;
    private readonly ITicketDataAccess _reservationDataAccess;
    public MovieProjectionDataAccess(AppDbContext context, ITicketDataAccess reservationDataAccess)
    {
        _context = context;
        _reservationDataAccess = reservationDataAccess;
    }
    
    public async Task<IEnumerable<MovieProjection>> GetMovieProjectionsAsync()
    {
        try
        {
            return await _context.MovieProjections
                .Include(projection => projection.Tickets)
                .Include(projection => projection.CinemaRoom)
                .Include(projection => projection.Movie)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<MovieProjection> GetMovieProjectionAsync(int id)
    {
        try
        {
            return await _context.MovieProjections
                .Include(projection => projection.Tickets)
                .Include(projection => projection.CinemaRoom)
                .Include(projection => projection.Movie)
                .FirstOrDefaultAsync(projection => projection.Id == id);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> CreateMovieProjectionAsync(MovieProjection projection)
    {
        try
        {
            var result = await _context.MovieProjections.AddAsync(projection);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return -1;
        }
    }

    public async Task<bool> CreateMovieProjectionsAsync(List<MovieProjection> projections)
    {
        try
        {
            await _context.MovieProjections.AddRangeAsync(projections);

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            _context.MovieProjections.RemoveRange(projections);
            return false;
        }
    }

    public async Task<bool> UpdateMovieProjectionAsync(int id, MovieProjection projection)
    {
        try
        {
            MovieProjection foundProjection = await GetMovieProjectionAsync(id);
            if (foundProjection is null)
                return false;

            foundProjection.SeatsLeft = projection.SeatsLeft;
            foundProjection.StartingTime = projection.StartingTime;
            foundProjection.EndingTime = projection.EndingTime;
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteMovieProjectionAsync(int id)
    {
        try
        {
            MovieProjection foundMovieProjection = await GetMovieProjectionAsync(id);
            foreach (Ticket reservation in foundMovieProjection.Tickets)
            {
                await _reservationDataAccess.DeleteTicketsAsync(reservation.Id);
            }
            _context.MovieProjections.Remove(foundMovieProjection);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
