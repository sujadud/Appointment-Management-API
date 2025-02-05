using Appointment_Management.Domain.Entities;
using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Management.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            if(username is null) throw new ArgumentNullException(nameof(username));
            return await _context.Users.FirstOrDefaultAsync(predicate: u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            if(user is not null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException(nameof(user));
            }
        }
    }
}
