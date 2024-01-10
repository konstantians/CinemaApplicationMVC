using CinemaApplication.SharedModels;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.DataAccess.Repositories;

public class ReservationDataAccess : IReservationDataAccess
{
    private readonly AppDbContext _context;
    public ReservationDataAccess(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reservation>> GetReservationsAsync()
    {
        try
        {
            return await _context.Reservations
                .Include(reservation => reservation.MovieProjection)
                .ThenInclude(projection => projection.CinemaRoom)
                .Include(reservation => reservation.MovieProjection)
                .ThenInclude(projection => projection.Movie)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Reservation> GetReservationAsync(int id)
    {
        try
        {
            return await _context.Reservations
                .Include(reservation => reservation.MovieProjection)
                .ThenInclude(projection => projection.CinemaRoom)
                .Include(reservation => reservation.MovieProjection)
                .ThenInclude(projection => projection.Movie)
                .FirstOrDefaultAsync(reservation => reservation.Id == id);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> CreateReservationAsync(Reservation reservation)
    {
        try
        {
            var result = await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task UpdateReservationAsync(int id, Reservation reservation)
    {
        try
        {
            Reservation foundReservation = await GetReservationAsync(id);
            if (foundReservation is null)
                return;

            foundReservation.ReservationRefoundPrice = reservation.ReservationRefoundPrice;
            foundReservation.NumberOfSeats = reservation.NumberOfSeats;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task DeleteReservationAsync(int id)
    {
        try
        {
            Reservation foundReservation = await GetReservationAsync(id);
            _context.Reservations.Remove(foundReservation);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
