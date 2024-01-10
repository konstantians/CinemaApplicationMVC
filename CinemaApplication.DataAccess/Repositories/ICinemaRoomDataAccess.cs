using CinemaApplication.SharedModels;

namespace CinemaApplication.DataAccess.Repositories
{
    public interface ICinemaRoomDataAccess
    {
        Task<int> CreateCinemaRoomAsync(CinemaRoom cinemaRoom);
        Task DeleteCinemaRoomAsync(int id);
        Task<CinemaRoom> GetCinemaRoomAsync(int id);
        Task<IEnumerable<CinemaRoom>> GetCinemaRoomsAsync();
        Task UpdateCinemaRoomAsync(int id, CinemaRoom cinemaRoom);
    }
}