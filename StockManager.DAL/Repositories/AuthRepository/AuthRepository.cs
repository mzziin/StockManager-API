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
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error Code: {error.Code}, Description: {error.Description}");
                }
            }
            return result.Succeeded;
        }

        public async Task<bool> CheckPassword(IdentityUser user, string password) => await _userManager.CheckPasswordAsync(user, password);

        public async Task<bool> CreateUser(IdentityUser user, string password) => (await _userManager.CreateAsync(user, password)).Succeeded;

        public async Task<bool> DeleteUser(IdentityUser user) => (await _userManager.DeleteAsync(user)).Succeeded;

        public async Task<IdentityUser?> GetUserById(Guid id) => await _userManager.FindByIdAsync(id.ToString());

        public async Task<IdentityUser?> GetUserByEmail(string email) => await _userManager.FindByEmailAsync(email);

        public async Task<IdentityUser?> GetUserByUserName(string userName) => await _userManager.FindByNameAsync(userName);

        public async Task<IList<string>> GetUserRoles(IdentityUser user) => await _userManager.GetRolesAsync(user);
    }
}
