using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PIMS.Domain.Entities;
using PIMS.Persistence.Context;


namespace PIMS.Persistence.Seed;

/// <summary>
/// Seeds default admin user.
/// </summary>
public static class AdminSeeder
{

    public static async Task SeedAsync(PimsDbContext context)
    {

        if (await context.Users.AnyAsync())
        {
            return;
        }


        var adminRole = await context.Roles
            .FirstOrDefaultAsync(x => x.Name == "Admin");


        if (adminRole == null)
        {
            throw new Exception("Admin role not found.");
        }



        var adminUser = new User
        {
            FirstName = "System",
            LastName = "Administrator",

            Email = "admin@pims.com",

            PhoneNumber = "9999999999",

            RoleId = adminRole.Id,

            IsActive = true
        };



        var passwordHasher = new PasswordHasher<User>();


        adminUser.PasswordHash =
            passwordHasher.HashPassword(
                adminUser,
                "Admin@123"
            );



        await context.Users.AddAsync(adminUser);


        await context.SaveChangesAsync();

    }
}