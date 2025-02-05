using Appointment_Management.Domain.Entities;

namespace Appointment_Management.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
    }
}