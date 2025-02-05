﻿using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Management.Infrastructure.Repositories
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class, IEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _entities;

        public CommonRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<bool> ExistsAsync(Guid doctorId)
        {
            return await _context.Doctors.AnyAsync(d => d.Id == doctorId);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
