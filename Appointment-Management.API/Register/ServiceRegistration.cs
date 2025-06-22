using Application;
using Application.Services;
using Application.Services.Auth;
using Application.Validation;
using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Infrastructure.Repositories;
using FluentValidation;
using Infrastructure;

namespace Appointment_Management.API.Register
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddInfrastructure(configuration);

        }
    }
}
