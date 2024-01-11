using CinemaApplication.SharedModels;

namespace CinemaApplication.AuthenticationAndAuthorization.Authentication;

public interface IAuthenticationProcedures
{
    bool CheckIfUserIsLoggedIn();
    Task<bool> DeleteUserAccountAsync(AppUser appUser);
    Task<AppUser> getCurrentUserAsync();
    Task LogOutUserAsync();
    Task RegisterUserAsync(AppUser appUser, string password, bool isPersistent, string roleName);
    Task<bool> SignInUserAsync(string username, string password, bool isPersistent);
    Task<bool> UpdateUserAccountAsync(AppUser appUser);
}