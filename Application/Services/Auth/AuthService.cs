using Application.Interfaces.IAuth;
using Appointment_Management.Domain.Entities;
using Appointment_Management.Domain.Entities.Enums;
using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Infrastructure.Data;
using Appointment_Management.Infrastructure.Repositories;
using Appointment_Management.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Auth;

public class AuthService : CommonRepository<User>, IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;
    private readonly TokenGenerator _tokenGenerator;

    public AuthService(AppDbContext context, IUserRepository userRepository, IConfiguration configuration, ICurrentUserService currentUserService)
        : base(context, currentUserService) // Pass the required 'currentUserService' parameter
    {
        _userRepository = userRepository;
        _config = configuration;
        _tokenGenerator = new TokenGenerator(_config);
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

    public async Task<bool> ChangePassword(string username, string newPassword)
    {
        User userOB = await _userRepository.GetUserByUsernameAsync(username);
        if (userOB == null)
            return false;

        var passwordHash = PasswordService.HashPassword(newPassword);
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

        var token = await this._tokenGenerator.GenerateJwtToken(user);
        return token;
    }
}