using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Persistence.Context;
using PIMS.Persistence.Repositories;

namespace PIMS.Persistence.DependencyInjection;

/// <summary>
/// Provides extension methods for registering persistence services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers persistence layer services.
    /// </summary>
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ============================================
        // Database Context
        // ============================================

        var connectionString =
            configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<PimsDbContext>(options =>
        {
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString));
        });

        // ============================================
        // Generic Repository
        // ============================================

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // ============================================
        // Unit Of Work
        // ============================================

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}