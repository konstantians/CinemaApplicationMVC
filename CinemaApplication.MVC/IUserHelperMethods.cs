using CinemaApplication.SharedModels;

namespace CinemaApplication.MVC
{
    public interface IUserHelperMethods
    {
        Task<bool> DeleteUserAndAssociatedInformation(string? userId = null);
        Task<AppUser> GetUserWithBankCards(string? userId = null);
        Task<AppUser> GetUserWithBankCardsAndTickets(string? userId = null);
        Task<AppUser> GetUserWithoutRelatedData(string? userId = null);
        Task<AppUser> GetUserWithTickets(string? userId = null);
    }
}