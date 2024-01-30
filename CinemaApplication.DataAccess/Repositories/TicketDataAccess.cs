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

    public async Task<IEnumerable<Ticket>> GetTicketsAsync()
    {
        try
        {
            return await _context.Tickets
                .Include(ticket => ticket.MovieProjection)
                .ThenInclude(projection => projection.CinemaRoom)
                .Include(ticket => ticket.MovieProjection)
                .ThenInclude(projection => projection.Movie)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Ticket>> GetTicketsOfUserAsync(string userId)
    {
        try
        {
            return await _context.Tickets
                .Include(ticket => ticket.MovieProjection)
                .ThenInclude(projection => projection.CinemaRoom)
                .Include(ticket => ticket.MovieProjection)
                .ThenInclude(projection => projection.Movie)
                .Where(ticket => ticket.UserId == userId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Ticket> GetTicketAsync(int id)
    {
        try
        {
            return await _context.Tickets
                .Include(ticket => ticket.MovieProjection)
                .ThenInclude(projection => projection.CinemaRoom)
                .Include(ticket => ticket.MovieProjection)
                .ThenInclude(projection => projection.Movie)
                .FirstOrDefaultAsync(ticket => ticket.Id == id);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> CreateTicketAsync(Ticket ticket)
    {
        try
        {
            var result = await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> UpdateTicketAsync(int id, Ticket ticket)
    {
        try
        {
            Ticket foundTicket = await GetTicketAsync(id);
            if (foundTicket is null)
                return false;

            foundTicket.ReservationRefundPrice = ticket.ReservationRefundPrice;
            foundTicket.NumberOfSeats = ticket.NumberOfSeats;
            await _context.SaveChangesAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteTicketsAsync(int id)
    {
        try
        {
            Ticket foundTicket = await GetTicketAsync(id);
            if (foundTicket is null)
                return false;

            _context.Tickets.Remove(foundTicket);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteTicketsOfUser(string userId)
    {
        try
        {
            var foundTickets = await GetTicketsOfUserAsync(userId);

            if (foundTickets != null && foundTickets.Any())
            {
                _context.Tickets.RemoveRange(foundTickets);
                await _context.SaveChangesAsync();
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
