
using CinemaApplication.AuthenticationAndAuthorization.Authorization;
using CinemaApplication.SharedModels;
using Microsoft.AspNetCore.Identity;
using CinemaApplication.AuthenticationAndAuthorization.Authentication;
using CinemaApplication.AuthenticationAndAuthorization;
using Microsoft.EntityFrameworkCore;
using System.Xml.XPath;

namespace AuthenticationAndAuthorization.Authentication;

public class AuthenticationProcedures : IAuthenticationProcedures
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppIdentityDbContext _appIdentityDbContext;
    private readonly IAuthorizationProcedures _authorizationProcedures;
    public AuthenticationProcedures(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        AppIdentityDbContext appIdentityDbContext, IAuthorizationProcedures authorizationProcedures)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _appIdentityDbContext = appIdentityDbContext;
        _authorizationProcedures = authorizationProcedures;

    }

    public async Task<AppUser> getCurrentUserAsync()
    {
        return await _userManager.GetUserAsync(_signInManager.Context.User);
    }

    public async Task<AppUser> FindByUserIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<AppUser> FindByUsernameAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<AppUser> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<(string, string)> RegisterUserAsync(AppUser appUser, string password, 
        bool isPersistent, string roleName)
    {
        var executionStrategy = _appIdentityDbContext.Database.CreateExecutionStrategy();
        string confirmationToken = null!;
        await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = await _appIdentityDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await _userManager.CreateAsync(appUser, password);

                    // If user creation fails, rollback the transaction
                    if (!result.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return;
                    }

                    IdentityRole role = await _authorizationProcedures.GetRoleByNameAsync(roleName);
                    bool roleResult = await _authorizationProcedures.AddRoleToUserAsync(appUser.Id, role.Id);

                    // If adding role fails, rollback the transaction
                    if (!roleResult)
                    {
                        await transaction.RollbackAsync();
                        return;
                    }

                    confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    // If everything is successful, commit the transaction
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    // If an exception occurs, rollback the transaction
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        });

        if (confirmationToken != null)
            return (appUser.Id, confirmationToken);
        else
            throw new InvalidOperationException("User registration failed.");
    }

    public async Task<string> CreateResetPasswordTokenAsync(AppUser appUser)
    {
        try
        {
            return await _userManager.GeneratePasswordResetTokenAsync(appUser);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> SignInUserAsync(string username, string password, bool isPersistent)
    {
        try
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent, false);
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public bool CheckIfUserIsLoggedIn()
    {
        return _signInManager.IsSignedIn(_signInManager.Context.User);
    }

    public async Task<bool> UpdateUserAccountAsync(AppUser appUser)
    {
        try
        {
            var result = await _userManager.UpdateAsync(appUser);
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task LogOutUserAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> DeleteUserAccountAsync(AppUser appUser)
    {
        try
        {
            var result = await _userManager.DeleteAsync(appUser);
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> ConfirmEmailAsync(string userId, string confirmationToken)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, confirmationToken);

            if (!result.Succeeded)
            {
                return false;
            }

            await _signInManager.SignInAsync(user, false);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> ResetPasswordAsync(string userId, string resetPasswordToken, string newPassword)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return false;
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordToken, newPassword);

            if (!result.Succeeded)
            {
                return false;
            }

            await _signInManager.SignInAsync(user, false);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
