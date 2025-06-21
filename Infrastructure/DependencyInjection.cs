using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Infrastructure.Repositories;
using Appointment_Management.Infrastructure.Data;
using Appointment_Management.Infrastructure.Services;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        ArgumentNullException.ThrowIfNull(nameof(connectionString));

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString, config =>
            {
                config.MigrationsAssembly(nameof(Infrastructure));
            });
        });

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
