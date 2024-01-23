namespace CinemaApplication.AuthenticationAndAuthorization.Authorization;

using CinemaApplication.SharedModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AuthorizationProcedures : IAuthorizationProcedures
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    public AuthorizationProcedures(RoleManager<IdentityRole> identityUserRole, UserManager<AppUser> userManager)
    {
        _roleManager = identityUserRole;
        _userManager = userManager;
    }

    public async Task<IEnumerable<IdentityRole>> GetRolesAsync(string roleId)
    {
        try
        {
            return await _roleManager.Roles.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<IdentityRole> GetRoleAsync(string roleId)
    {
        try
        {
            var foundRole = await _roleManager.FindByIdAsync(roleId);
            if (foundRole is null)
            {
                Console.WriteLine($"role with {roleId} was not found.");
            }
            return foundRole;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<IdentityRole> GetRoleByNameAsync(string roleName)
    {
        try
        {
            var foundRole = await _roleManager.FindByNameAsync(roleName);
            if (foundRole is null)
            {
                Console.WriteLine($"role with {roleName} was not found.");
            }
            return foundRole;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<string> GetUserRoleAsync(string userId)
    {
        var foundUser = await _userManager.FindByIdAsync(userId);
        if (foundUser is null)
            return null;

        var foundRoles = await _userManager.GetRolesAsync(foundUser);
        if (foundRoles is null)
            return null;

        return foundRoles.FirstOrDefault();
    }

    public async Task CreateRoleAsync(string roleName)
    {
        try
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        //exception for duplicate admin role
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Role with name '{roleName}' already exists.");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task UpdateRoleAsync(string roleId, string roleName)
    {
        try
        {
            var foundRole = await GetRoleAsync(roleId);
            if (foundRole is null)
                return;

            foundRole.Name = roleName;
            await _roleManager.UpdateAsync(foundRole);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task DeleteRoleAsync(string roleId)
    {
        try
        {
            var foundRole = await GetRoleAsync(roleId);
            if (foundRole is null)
                return;

            await _roleManager.DeleteAsync(foundRole);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<IEnumerable<AppUser>> GetUsersOfRoleAsync(string roleId)
    {
        try
        {
            var foundRole = await GetRoleAsync(roleId);
            if (foundRole is null)
                return null;

            //Keep fixing this
            IList<AppUser> usersInRole = await _userManager.GetUsersInRoleAsync(foundRole.Name);
            return usersInRole;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> AddRoleToUserAsync(string userId, string roleId)
    {
        try
        {
            (var wasFound, var foundUser, var foundRole) = (await DoesTheInformationExist(userId, roleId));
            if (!wasFound)
                return false;

            var result = await _userManager.AddToRoleAsync(foundUser, foundRole.Name);
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task RemoveRoleFromUserAsync(string userId, string roleId)
    {
        try
        {
            (var wasFound, var foundUser, var foundRole) = (await DoesTheInformationExist(userId, roleId));
            if (!wasFound)
                return;

            await _userManager.RemoveFromRoleAsync(foundUser, foundRole.Name);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    private async Task<(bool wasFound, AppUser appUser, IdentityRole identityRole)> DoesTheInformationExist(string userId, string roleId)
    {
        var foundUser = await _userManager.FindByIdAsync(userId);
        if (foundUser is null)
        {
            Console.WriteLine($"user with {userId} was not found");
            return (false, null, null);
        }

        var foundRole = await _roleManager.FindByIdAsync(roleId);
        if (foundRole is null)
        {
            Console.WriteLine($"role with {roleId} was not found");
            return (false, null, null);
        }

        return (true, foundUser, foundRole);
    }

    
}

