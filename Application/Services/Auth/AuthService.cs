using Application.Interfaces.IAuth;
using Application.Services;
using Appointment_Management.Domain.Entities;
using Appointment_Management.Domain.Entities.Enums;
using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Infrastructure.Data;
using Appointment_Management.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Auth
{
    public class AuthService : CommonRepository<User>, IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IUserRepository userRepository, IConfiguration configuration)
            : base(context)
        {
            _userRepository = userRepository;
            _config = configuration;
        }

        public async Task<bool> RegisterUser(string username, string password, RoleType role)
        {
            User userOB = await _userRepository.GetUserByUsernameAsync(username);
            if (userOB == null)
                return false;

            var passwordHash = PasswordService.HashPassword(password);
            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                Role = role
            };
            await _userRepository.AddUserAsync(user);

            return true;
        }

        public async Task<bool> ChangePassword(ClaimsIdentity identity, string username, string newPassword)
        {
            User userOB = await _userRepository.GetUserByUsernameAsync(username);
            if (userOB == null)
                return false;

            var passwordHash = PasswordService.HashPassword(newPassword);
            //var user = new User
            //{
            //    Id = userOB.Id,
            //    Username = userOB.Username,
            //    PasswordHash = passwordHash,
            //};
            userOB.PasswordHash = passwordHash;

            try
            {
                await UpdateAsync(userOB);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return false;
            }

            return true;
        }

        public async Task<string> AuthenticateUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !PasswordService.VerifyPassword(password, user.PasswordHash))
                return null;

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var key = _config["JwtSettings:Secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            return new JwtSecurityTokenHandler()
                            .WriteToken(new JwtSecurityTokenHandler()
                            .CreateToken(tokenDescriptor));
        }
    }
}
