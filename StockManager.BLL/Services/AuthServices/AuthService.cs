using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs;
using StockManager.DAL.Repositories.AuthRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockManager.BLL.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepository, IConfiguration configurationManager)
        {
            _authRepository = authRepository;
            _configuration = configurationManager;
        }
        public async Task<ResponseModel<UserDto>> LoginUser(LoginModel loginModel)
        {
            var user = await _authRepository.GetUserByUserName(loginModel.Username);

            if (user != null && await _authRepository.CheckPassword(user, loginModel.Password))
            {
                var userRoles = await _authRepository.GetUserRoles(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return new ResponseModel<UserDto>
                {
                    Status = true,
                    Message = "User logged in successfully",
                    Data = new UserDto
                    {
                        Username = loginModel.Username,
                        Email = user.Email!,
                        Roles = userRoles,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                    }
                };
            }
            else
            {
                return new ResponseModel<UserDto>
                {
                    Status = false,
                    Message = "Incorrect username or password"
                };
            }
        }

        public async Task<ResponseModel<object>> RegisterUserOrAdmin(RegisterModel registerModel, string role)
        {
            var userExists1 = await _authRepository.GetUserByUserName(registerModel.Username);
            if (userExists1 != null)
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "username already exists"
                };

            var userExists2 = await _authRepository.GetUserByEmail(registerModel.Email);
            if (userExists2 != null)
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "Email already exists"
                };

            IdentityUser user = new()
            {
                UserName = registerModel.Username,
                Email = registerModel.Email,
                PhoneNumber = registerModel.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString()
            };


            if (await _authRepository.CreateUser(user, registerModel.Password))
            {
                if (await _authRepository.AddRoleToUser(user, role))
                    return new ResponseModel<object>
                    {
                        Status = true,
                        Message = $"{role} registered succssfully"
                    };
                else
                    await _authRepository.DeleteUser(user);
            }

            return new ResponseModel<object>
            {
                Status = false,
                Message = "Something went wrong",
            };
        }

        // generate JWT Token
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
