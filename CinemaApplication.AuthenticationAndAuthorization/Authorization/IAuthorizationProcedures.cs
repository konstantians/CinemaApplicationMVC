using CinemaApplication.SharedModels;
using Microsoft.AspNetCore.Identity;

namespace CinemaApplication.AuthenticationAndAuthorization.Authorization
{
    public interface IAuthorizationProcedures
    {
        Task<bool> AddRoleToUserAsync(string userId, string roleId);
        Task CreateRoleAsync(string roleName);
        Task DeleteRoleAsync(string roleId);
        Task<IdentityRole> GetRoleAsync(string roleId);
        Task<IdentityRole> GetRoleByNameAsync(string roleName);
        Task<IEnumerable<IdentityRole>> GetRolesAsync(string roleId);
        Task<IEnumerable<AppUser>> GetUsersWithRoleAsync(string roleId);
        Task RemoveRoleFromUserAsync(string userId, string roleId);
        Task UpdateRoleAsync(string roleId, string roleName);
    }
}