using Microsoft.AspNetCore.Identity;

namespace StockManager.DAL.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        Task<IdentityUser?> GetUserById(Guid id);
        Task<IdentityUser?> GetUserByUserName(string userName);
        Task<IdentityUser?> GetUserByEmail(string email);
        Task<bool> CheckPassword(IdentityUser user, string password);
        Task<IList<string>> GetUserRoles(IdentityUser user);
        Task<bool> CreateUser(IdentityUser user, string password);
        Task<bool> DeleteUser(IdentityUser user);
        Task<bool> AddRoleToUser(IdentityUser user, string role);
    }
}
