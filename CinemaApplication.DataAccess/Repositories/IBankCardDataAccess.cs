using CinemaApplication.SharedModels;

namespace CinemaApplication.DataAccess.Repositories
{
    public interface IBankCardDataAccess
    {
        Task<int> CreateBankCardAsync(BankCard bankCard);
        Task<bool> DeleteBankCardAsync(int id);
        Task<bool> DeleteBankCardsOfUserAsync(string userId);
        Task<BankCard> GetBankCardAsync(int id);
        Task<IEnumerable<BankCard>> GetBankCardsAsync();
        Task<IEnumerable<BankCard>> GetBankCardsOfUserAsync(string userId);
        Task<bool> UpdateBankCardAsync(int id, BankCard bankCard);
    }
}