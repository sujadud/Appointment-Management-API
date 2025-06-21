using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Domain.Interfaces.IAudit;
using Appointment_Management.Infrastructure.Data;
using Appointment_Management.Infrastructure.Services;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Appointment_Management.Infrastructure.Repositories
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class, IEntity
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<T> _entities;
        private readonly ICurrentUserService _currentUserService;

        public CommonRepository(AppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _entities = context.Set<T>();
            _currentUserService = currentUserService;
        }

        public virtual async Task<bool> ExistsAsync(Guid doctorId)
        {
            return await _context.Doctors.AnyAsync(d => d.Id == doctorId);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();
            var now = DateTime.UtcNow;

            entity.CreatedBy = currentUserId;
            entity.CreatedAt = now;
            entity.UpdatedBy = currentUserId;
            entity.UpdatedAt = now;

            await _entities.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();
            entity.UpdatedBy = currentUserId;
            entity.UpdatedAt = DateTime.UtcNow;

            _entities.Update(entity);
            await Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
        }

        public virtual async Task SaveAsync()
        {
            // Update audit fields for modified entities before saving
            var entries = _context.ChangeTracker.Entries<IEntity>()
                .Where(e => e.State == EntityState.Modified);

            var currentUserId = _currentUserService.GetCurrentUserId();
            var now = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                entry.Entity.UpdatedBy = currentUserId;
                entry.Entity.UpdatedAt = now;
            }

            await _context.SaveChangesAsync();
        }
    }
}
