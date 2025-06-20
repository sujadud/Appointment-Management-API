using Appointment_Management.Application.Services;
using Appointment_Management.Application.Services.Auth;
using Appointment_Management.Application.Validation;
using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Infrastructure.Repositories;
using FluentValidation;

namespace Appointment_Management.API.Register
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
            services.AddScoped<AppointmentService>();
            services.AddScoped<DoctorService>();

            // Auth Services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<AuthService>();
            services.AddSingleton<PasswordService>();

            // Validation Services
            services.AddValidatorsFromAssemblyContaining<AppointmentValidator>();
        }
    }
}
