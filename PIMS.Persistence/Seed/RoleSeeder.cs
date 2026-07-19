using Microsoft.EntityFrameworkCore;
using PIMS.Domain.Entities;
using PIMS.Persistence.Context;

namespace PIMS.Persistence.Seed;

/// <summary>
/// Seeds default application roles.
/// </summary>
public static class RoleSeeder
{
    public static async Task SeedAsync(PimsDbContext context)
    {
        if (await context.Roles.AnyAsync())
        {
            return;
        }


        var roles = new List<Role>
        {
            new Role
            {
                Name = "Admin",
                Description = "Administrator with full system access",
                IsActive = true
            },

            new Role
            {
                Name = "Manager",
                Description = "Manages inventory operations",
                IsActive = true
            },

            new Role
            {
                Name = "Employee",
                Description = "Handles daily inventory activities",
                IsActive = true
            }
        };


        await context.Roles.AddRangeAsync(roles);

        await context.SaveChangesAsync();
    }
}