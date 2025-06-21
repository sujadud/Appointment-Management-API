namespace Appointment_Management.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        Guid GetCurrentUserId();
    }
}