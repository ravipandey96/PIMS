using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Persistence.Context;
using PIMS.Persistence.Repositories;
using PIMS.Persistence.UnitOfWork1;

namespace PIMS.Persistence;

/// <summary>
/// Provides extension methods for registering persistence services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers persistence layer services.
    /// </summary>
    /// <param name="services">
    /// Service collection.
    /// </param>
    /// <param name="configuration">
    /// Application configuration.
    /// </param>
    /// <returns>
    /// Updated service collection.
    /// </returns>
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DbContext with MySQL provider
        services.AddDbContext<PimsDbContext>(options =>
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection");

            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString));
        });


        // Register generic repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


        // Register entity repositories
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IInventoryRepository, InventoryRepository>();


        // Register Unit Of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();


        return services;
    }
}