using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PIMS.Application.Interfaces.Authentication;
using PIMS.Infrastructure.Authentication;
using PIMS.Infrastructure.Configurations;

namespace PIMS.Infrastructure.DependencyInjection;

/// <summary>
/// Provides extension methods for registering infrastructure services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers infrastructure services.
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
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // =========================================================
        // JWT Configuration
        // =========================================================

        services.Configure<JwtOptions>(
            configuration.GetSection("Jwt"));

        var jwtOptions =
            configuration.GetSection("Jwt")
                         .Get<JwtOptions>()
            ?? throw new InvalidOperationException(
                "JWT configuration is missing.");

        // =========================================================
        // Authentication
        // =========================================================

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                options.SaveToken = true;

                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtOptions.Key)),

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.Zero
                    };
            });

        // =========================================================
        // Authorization
        // =========================================================

        services.AddAuthorization();

        // =========================================================
        // Security Services
        // =========================================================

        services.AddScoped<IJwtService, JwtService>();

        services.AddScoped<IRefreshTokenService,
            RefreshTokenService>();

        services.AddScoped<IPasswordHasher,
            PasswordHasher>();

        // =========================================================
        // Application Services
        // =========================================================

        /*services.AddScoped<IAuthenticationService,
            AuthenticationService>();*/

        return services;
    }
}