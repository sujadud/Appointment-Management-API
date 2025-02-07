﻿using Appointment_Management.API.Domain.Entities.Enums;
using Appointment_Management.Application.Services;
using Appointment_Management.Domain.Entities;
using Appointment_Management.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Appointment_Management.Application.Services.Auth
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config = configuration;
        }

        public async Task<bool> RegisterUser(string username, string password, RoleType role = RoleType.User)
        {
            if (await _userRepository.GetUserByUsernameAsync(username) != null)
                return false; // Username already exists

            var passwordHash = PasswordService.HashPassword(password, out string salt);
            var user = new User 
            { 
                Username = username, 
                PasswordHash = passwordHash, 
                Salt = salt, 
                Role = role
            };
            await _userRepository.AddUserAsync(user);
            return true;
        }

        public async Task<string> AuthenticateUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !PasswordService.VerifyPassword(password, user.PasswordHash, user.Salt))
                return null;

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var key = _config["JwtSettings:Secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
