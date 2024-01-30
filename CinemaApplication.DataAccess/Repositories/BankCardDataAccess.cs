using CinemaApplication.SharedModels;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.DataAccess.Repositories;

public class BankCardDataAccess : IBankCardDataAccess
{
    private readonly AppDbContext _context;
    public BankCardDataAccess(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BankCard>> GetBankCardsAsync()
    {
        try
        {
            return await _context.BankCards.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<BankCard>> GetBankCardsOfUserAsync(string userId)
    {
        try
        {
            return await _context.BankCards.Where(bankCard => bankCard.UserId == userId).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }


    public async Task<BankCard> GetBankCardAsync(int id)
    {
        try
        {
            return await _context.BankCards.FirstOrDefaultAsync(bankCard => bankCard.Id == id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> CreateBankCardAsync(BankCard bankCard)
    {
        try
        {
            var result = await _context.BankCards.AddAsync(bankCard);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> UpdateBankCardAsync(int id, BankCard bankCard)
    {
        try
        {
            BankCard foundBankCard = await GetBankCardAsync(id);
            if (foundBankCard is null)
                return false;

            foundBankCard.CardNumber = bankCard.CardNumber;
            foundBankCard.CardHolderName = bankCard.CardHolderName;
            foundBankCard.ExpirationDate = bankCard.ExpirationDate;
            foundBankCard.CVC = bankCard.CVC;
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteBankCardAsync(int id)
    {
        try
        {
            BankCard foundBankCard = await GetBankCardAsync(id);
            if (foundBankCard is null)
                return false;

            _context.BankCards.Remove(foundBankCard);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteBankCardsOfUserAsync(string userId)
    {
        try
        {
            var foundBankCards = await GetBankCardsOfUserAsync(userId);

            if (foundBankCards != null && foundBankCards.Any())
            {
                _context.BankCards.RemoveRange(foundBankCards);
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
