using CinemaApplication.SharedModels;
using Microsoft.AspNetCore.Identity;

namespace CinemaApplication.AuthenticationAndAuthorization.Authentication;

public class AuthenticationProcedures
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    public AuthenticationProcedures(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<AppUser> getCurrentUserAsync()
    {
        return await _userManager.GetUserAsync(_signInManager.Context.User);
    }

    public async Task RegisterUserAsync(AppUser appUser, string password, bool isPersistent)
    {
        try
        {
            var result = await _userManager.CreateAsync(appUser, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent);
            }
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
}
