using Microsoft.AspNetCore.Identity;

namespace StockManager.DAL.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        public Task<IdentityUser?> GetUserByUserName(string userName);
        public Task<IdentityUser?> GetUserByEmail(string email);
        public Task<bool> CheckPassword(IdentityUser user, string password);
        public Task<IList<string>> GetUserRoles(IdentityUser user);
        public Task<bool> CreateUser(IdentityUser user, string password);
        public Task<bool> DeleteUser(IdentityUser user);
        public Task<bool> AddRoleToUser(IdentityUser user, string role);
    }
}
