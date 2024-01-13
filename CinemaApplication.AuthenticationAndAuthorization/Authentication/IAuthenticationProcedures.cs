using CinemaApplication.SharedModels;

namespace CinemaApplication.AuthenticationAndAuthorization.Authentication;

public interface IAuthenticationProcedures
{
    bool CheckIfUserIsLoggedIn();
    Task<bool> DeleteUserAccountAsync(AppUser appUser);
    Task<AppUser> getCurrentUserAsync();
    Task<AppUser> FindByUserIdAsync(string userId);
    Task<AppUser> FindByUsernameAsync(string username);
    Task<AppUser> FindByEmailAsync(string email);
    Task LogOutUserAsync();
    Task<(string userId, string ConfirmationToken)> RegisterUserAsync(AppUser appUser, string password, bool isPersistent, string roleName);
    Task<bool> SignInUserAsync(string username, string password, bool isPersistent);
    Task<bool> UpdateUserAccountAsync(AppUser appUser);
    Task<bool> ConfirmEmailAsync(string userId, string confirmationToken);
    Task<string> CreateResetPasswordTokenAsync(AppUser appUser);
    Task<bool> ResetPasswordAsync(string userId, string resetPasswordToken, string newPassword);
}