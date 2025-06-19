using Appointment_Management.Domain.Entities.Enums;

namespace Application.Interfaces.IAuth
{
    public interface IAuthService
    {
        Task<string> AuthenticateUser(string username, string password);
        Task<bool> RegisterUser(string username, string password, RoleType role);
    }
}