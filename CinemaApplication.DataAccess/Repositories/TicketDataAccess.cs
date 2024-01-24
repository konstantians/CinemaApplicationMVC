using CinemaApplication.SharedModels;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.DataAccess.Repositories;

public class TicketDataAccess : ITicketDataAccess
{
    private readonly AppDbContext _context;
    public TicketDataAccess(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Ticket>> GetReservationsAsync()
    {
        try
        {
            return await _context.Tickets
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

    public async Task<Ticket> GetReservationAsync(int id)
    {
        try
        {
            return await _context.Tickets
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

    public async Task<int> CreateReservationAsync(Ticket reservation)
    {
        try
        {
            var result = await _context.Tickets.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task UpdateReservationAsync(int id, Ticket reservation)
    {
        try
        {
            Ticket foundReservation = await GetReservationAsync(id);
            if (foundReservation is null)
                return;

            foundReservation.ReservationRefundPrice = reservation.ReservationRefundPrice;
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
            Ticket foundReservation = await GetReservationAsync(id);
            _context.Tickets.Remove(foundReservation);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
