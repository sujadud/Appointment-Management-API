using Application.Services;
using Application.Services.Auth;
using Application.Validation;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AppointmentService>();
        services.AddScoped<DoctorService>();

        // Auth Services
        services.AddScoped<AuthService>();
        services.AddSingleton<PasswordService>();

        // Validation Services
        services.AddValidatorsFromAssemblyContaining<AppointmentValidator>();
    }
}
