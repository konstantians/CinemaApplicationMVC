using CinemaApplication.SharedModels;

namespace CinemaApplication.DataAccess.Repositories;

public interface ITicketDataAccess
{
    Task<int> CreateTicketAsync(Ticket reservation);
    Task<bool> DeleteTicketsAsync(int id);
    Task<bool> DeleteTicketsOfUser(string userId);
    Task<Ticket> GetTicketAsync(int id);
    Task<IEnumerable<Ticket>> GetTicketsAsync();
    Task<IEnumerable<Ticket>> GetTicketsOfUserAsync(string userId);
    Task<bool> UpdateTicketAsync(int id, Ticket reservation);
}