using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Domain.Interfaces.IAudit;
using Appointment_Management.Infrastructure.Data;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Appointment_Management.Infrastructure.Repositories
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class, IEntity
    {
        private readonly AppDbContext _context;
        public readonly DbSet<T> _entities;

        public CommonRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
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
            await _entities.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            //Could you please analyze the IEntity for a specific reason? I have updated it by adding timestamps. When someone attempts to add or update a value for any entity that extends the base IEntity, it will automatically populate the CreatedAt and CreatedBy fields when adding a new value, and the UpdatedAt and UpdatedBy fields when updating an existing value.
            //Can we update that into CommonRepository, or if you have any suggestions?
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
            await _context.SaveChangesAsync();
        }
    }
}
