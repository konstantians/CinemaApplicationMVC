using CinemaApplication.SharedModels;

namespace CinemaApplication.DataAccess.Repositories
{
    public interface IReservationDataAccess
    {
        Task<int> CreateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int id);
        Task<Reservation> GetReservationAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task UpdateReservationAsync(int id, Reservation reservation);
    }
}