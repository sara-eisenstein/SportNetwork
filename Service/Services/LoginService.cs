using Common.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IService<UserDto> _userService;
        private readonly IConfiguration _configuration;

        public LoginService(IService<UserDto> userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public string Authenticate(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("Email and password must be provided.");
                }

                var user = _userService.GetAll()
                    .FirstOrDefault(x => x.Email == email && x.PasswordHash == password);
                if (user == null)
                {
                    return null; // אם אין משתמש מתאים, נחזיר NULL
                }

                return GenerateToken(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during authentication: {ex.Message}");
            }
        }

        private string GenerateToken(UserDto user)
        {
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                };

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating JWT token: {ex.Message}");
            }
        }
    }
}

