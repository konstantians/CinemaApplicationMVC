using CinemaApplication.AuthenticationAndAuthorization.Authentication;
using CinemaApplication.AuthenticationAndAuthorization.Authorization;
using CinemaApplication.DataAccess.Repositories;
using CinemaApplication.SharedModels;

namespace CinemaApplication.MVC;

public class UserHelperMethods : IUserHelperMethods
{
    private readonly IAuthenticationProcedures _authenticationProcedures;
    private readonly IAuthorizationProcedures _authorizationProcedures;
    private readonly IBankCardDataAccess _bankCardDataAccess;
    private readonly ITicketDataAccess _ticketDataAccess;

    public UserHelperMethods(IAuthenticationProcedures authenticationProcedures, IAuthorizationProcedures authorizationProcedures,
        IBankCardDataAccess bankCardDataAccess, ITicketDataAccess ticketDataAccess)
    {
        _authenticationProcedures = authenticationProcedures;
        _authorizationProcedures = authorizationProcedures;
        _bankCardDataAccess = bankCardDataAccess;
        _ticketDataAccess = ticketDataAccess;
    }

    public async Task<AppUser> GetUserWithBankCardsAndTickets(string? userId = null)
    {
        AppUser user;
        if (userId is null)
            user = await _authenticationProcedures.GetCurrentUserAsync();
        else
            user = await _authenticationProcedures.FindByUserIdAsync(userId);

        if (user is null)
            return null;

        var result = await _bankCardDataAccess.GetBankCardsOfUserAsync(user.Id);
        user.BankCards = result.ToList();

        var result2 = await _ticketDataAccess.GetTicketsOfUserAsync(user.Id);
        user.Tickets = result2.ToList();

        return user;
    }

    public async Task<AppUser> GetUserWithBankCards(string? userId = null)
    {
        AppUser user;
        if (userId is null)
            user = await _authenticationProcedures.GetCurrentUserAsync();
        else
            user = await _authenticationProcedures.FindByUserIdAsync(userId);

        if (user is null)
            return null;

        var bankCards = await _bankCardDataAccess.GetBankCardsOfUserAsync(user.Id);
        user.BankCards = bankCards.ToList();

        return user;
    }

    public async Task<AppUser> GetUserWithTickets(string? userId = null)
    {
        AppUser user;
        if (userId is null)
            user = await _authenticationProcedures.GetCurrentUserAsync();
        else
            user = await _authenticationProcedures.FindByUserIdAsync(userId);

        if (user is null)
            return null;

        var tickets = await _ticketDataAccess.GetTicketsOfUserAsync(user.Id);
        user.Tickets = tickets.ToList();

        return user;
    }

    public async Task<AppUser> GetUserWithoutRelatedData(string? userId = null)
    {
        AppUser user;
        if (userId is null)
            user = await _authenticationProcedures.GetCurrentUserAsync();
        else
            user = await _authenticationProcedures.FindByUserIdAsync(userId);

        return user;
    }

    public async Task<bool> DeleteUserAndAssociatedInformation(string? userId = null)
    {
        AppUser user;
        if (userId is null)
            user = await _authenticationProcedures.GetCurrentUserAsync();

        else
            user = await _authenticationProcedures.FindByUserIdAsync(userId);

        if (user is null)
            return false;

        await _ticketDataAccess.DeleteTicketsOfUser(user.Id);
        await _bankCardDataAccess.DeleteBankCardsOfUserAsync(user.Id);
        await _authenticationProcedures.DeleteUserAccountAsync(user);

        return true;
    }
}
