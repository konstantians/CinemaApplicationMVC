using CinemaApplication.SharedModels;

namespace CinemaApplication.DataAccess.Repositories;

public interface ITicketDataAccess
{
    Task<int> CreateReservationAsync(Ticket reservation);
    Task DeleteReservationAsync(int id);
    Task<Ticket> GetReservationAsync(int id);
    Task<IEnumerable<Ticket>> GetReservationsAsync();
    Task UpdateReservationAsync(int id, Ticket reservation);
}