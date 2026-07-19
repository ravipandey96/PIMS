using Microsoft.EntityFrameworkCore;
using PIMS.Domain.Entities;
using PIMS.Persistence.Context;


namespace PIMS.Persistence.Seed;

/// <summary>
/// Seeds default product categories.
/// </summary>
public static class CategorySeeder
{
    public static async Task SeedAsync(PimsDbContext context)
    {
        if (await context.Categories.AnyAsync())
        {
            return;
        }


        var categories = new List<Category>
        {
            new Category
            {
                Name="Electronics",
                Description="Electronic products",
                IsActive=true
            },

            new Category
            {
                Name="Furniture",
                Description="Office and home furniture",
                IsActive=true
            },

            new Category
            {
                Name="Stationery",
                Description="Office stationery items",
                IsActive=true
            },

            new Category
            {
                Name="Accessories",
                Description="Computer and mobile accessories",
                IsActive=true
            },

            new Category
            {
                Name="Groceries",
                Description="Daily use grocery items",
                IsActive=true
            }
        };


        await context.Categories.AddRangeAsync(categories);

        await context.SaveChangesAsync();
    }
}