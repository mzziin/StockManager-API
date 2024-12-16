using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace StockManager.DAL.Repositories.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthRepository(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<bool> AddRoleToUser(IdentityUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> CheckPassword(IdentityUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> CreateUser(IdentityUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUser(IdentityUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<IdentityUser?> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityUser?> GetUserByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<IList<string>> GetUserRoles(IdentityUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
