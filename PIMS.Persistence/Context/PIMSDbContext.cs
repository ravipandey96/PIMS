using Microsoft.EntityFrameworkCore;
using PIMS.Domain.Entities;

namespace PIMS.Persistence.Context;

/// <summary>
/// Represents the application's database context.
/// </summary>
public class PimsDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PimsDbContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PimsDbContext(DbContextOptions<PimsDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the Roles table.
    /// </summary>
    public DbSet<Role> Roles => Set<Role>();

    /// <summary>
    /// Gets or sets the Users table.
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Gets or sets the Categories table.
    /// </summary>
    public DbSet<Category> Categories => Set<Category>();

    /// <summary>
    /// Gets or sets the Products table.
    /// </summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    /// Gets or sets the ProductCategories table.
    /// </summary>
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();

    /// <summary>
    /// Gets or sets the Inventories table.
    /// </summary>
    public DbSet<Inventory> Inventories => Set<Inventory>();

    /// <summary>
    /// Gets or sets the InventoryTransactions table.
    /// </summary>
    public DbSet<InventoryTransaction> InventoryTransactions => Set<InventoryTransaction>();

    /// <summary>
    /// Configures the entity model.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Automatically apply all IEntityTypeConfiguration<T>
        // classes from this assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PimsDbContext).Assembly);
    }
}