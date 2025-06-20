using Appointment_Management.Domain.Entities;
using Appointment_Management.Domain.Entities.Enums;
using Appointment_Management.Domain.Interfaces;

namespace Application.Interfaces.IAuth
{
    public interface IAuthService : ICommonRepository<User>
    {
        Task<string> AuthenticateUser(string username, string password);
        Task<bool> RegisterUser(string username, string password, RoleType role);
    }
}