using CinemaApplication.SharedModels;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.DataAccess.Repositories;

public class CinemaRoomDataAccess : ICinemaRoomDataAccess
{
    private readonly AppDbContext _context;
    private readonly IMovieProjectionDataAccess _movieProjectionDataAccess;
    public CinemaRoomDataAccess(AppDbContext context, IMovieProjectionDataAccess movieProjectionDataAccess)
    {
        _context = context;
        _movieProjectionDataAccess = movieProjectionDataAccess;
    }

    public async Task<IEnumerable<CinemaRoom>> GetCinemaRoomsAsync()
    {
        try
        {
            return await _context.CinemaRooms
                .Include(cinameRoom => cinameRoom.Projections)
                .ThenInclude(projection => projection.Movie)
                .Include(cinemaRoom => cinemaRoom.Projections)
                .ThenInclude(projection => projection.Tickets)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<CinemaRoom> GetCinemaRoomAsync(int id)
    {
        try
        {
            return await _context.CinemaRooms
                .Include(cinameRoom => cinameRoom.Projections)
                .ThenInclude(projection => projection.Movie)
                .Include(cinemaRoom => cinemaRoom.Projections)
                .ThenInclude(projection => projection.Tickets)
                .FirstOrDefaultAsync(cinemaRoom => cinemaRoom.Id == id);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> CreateCinemaRoomAsync(CinemaRoom cinemaRoom)
    {
        try
        {
            var result = await _context.CinemaRooms.AddAsync(cinemaRoom);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task UpdateCinemaRoomAsync(int id, CinemaRoom cinemaRoom)
    {
        try
        {
            CinemaRoom foundCinemaRoom = await GetCinemaRoomAsync(id);
            if (foundCinemaRoom is null)
                return;

            foundCinemaRoom.Name = cinemaRoom.Name;
            foundCinemaRoom.AvailableSeats = cinemaRoom.AvailableSeats;
            foundCinemaRoom.Supports3D = cinemaRoom.Supports3D;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task DeleteCinemaRoomAsync(int id)
    {
        try
        {
            CinemaRoom foundCinemaRoom = await GetCinemaRoomAsync(id);
            foreach (MovieProjection projection in foundCinemaRoom.Projections)
            {
                await _movieProjectionDataAccess.DeleteMovieProjectionAsync(projection.Id);
            }
            _context.CinemaRooms.Remove(foundCinemaRoom);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}

