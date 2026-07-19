using PIMS.Persistence.Context;


namespace PIMS.Persistence.Seed;

/// <summary>
/// Executes all database seed operations.
/// </summary>
public static class DatabaseSeeder
{

    public static async Task SeedAsync(PimsDbContext context)
    {

        await RoleSeeder.SeedAsync(context);


        await AdminSeeder.SeedAsync(context);


        await CategorySeeder.SeedAsync(context);

    }

}