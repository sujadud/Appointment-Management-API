using Appointment_Management.Domain.Interfaces.IAudit;

namespace Appointment_Management.Domain.Interfaces
{
    public interface ICommonRepository<T> where T : class, IEntity
    {
        Task<bool> ExistsAsync(Guid doctorId);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task SaveAsync();
    }
}
