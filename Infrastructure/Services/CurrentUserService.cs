using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Appointment_Management.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }

            // For system operations or when no user is authenticated, return a default system user ID
            // You might want to configure this value in your application settings
            return Guid.Parse("00000000-0000-0000-0000-000000000000");
        }
    }
}