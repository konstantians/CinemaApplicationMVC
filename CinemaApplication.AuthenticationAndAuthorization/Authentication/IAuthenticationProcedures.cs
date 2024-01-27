using CinemaApplication.SharedModels;

namespace CinemaApplication.AuthenticationAndAuthorization.Authentication;

public interface IAuthenticationProcedures
{
    Task<bool> CheckIfUserIsLoggedIn();
    Task<bool> DeleteUserAccountAsync(AppUser appUser);
    Task<AppUser> GetCurrentUserAsync();
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
    Task<bool> ChangePasswordAsync(AppUser appUser, string currentPassword, string newPassword);
    Task<string> CreateChangeEmailTokenAsync(AppUser appUser, string newEmail);
    Task<bool> ChangeEmailAsync(string userId, string changeEmailToken, string newEmail);
    Task<List<AppUser>> GetUsersAsync();
    Task<bool> RegisterUserAndConfirmEmailAsync(AppUser appUser, string password, bool isPersistent, string roleName);
    Task<bool> ChangePasswordWithoutConfirmationAsync(AppUser appUser, string newPassword);
    Task<bool> EnableOrDisableEmailWithoutConfirmationAsync(string userId, bool shouldActivate);
    Task<bool> ChangeEmailWithoutConfirmationAsync(string userId, string newEmail);
}