
using CinemaApplication.AuthenticationAndAuthorization.Authorization;
using CinemaApplication.SharedModels;
using Microsoft.AspNetCore.Identity;
using CinemaApplication.AuthenticationAndAuthorization.Authentication;
using CinemaApplication.AuthenticationAndAuthorization;
using Microsoft.EntityFrameworkCore;

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

    public async Task RegisterUserAsync(AppUser appUser, string password, bool isPersistent, string roleName)
    {
        var executionStrategy = _appIdentityDbContext.Database.CreateExecutionStrategy();

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

                    await _signInManager.SignInAsync(appUser, isPersistent);

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
}
