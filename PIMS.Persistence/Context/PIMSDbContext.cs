using Microsoft.EntityFrameworkCore;
using PIMS.Domain.Common;
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
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

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

    /// <summary>
    /// Saves all changes made in this context and automatically updates audit fields.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token.
    /// </param>
    /// <returns>
    /// The number of state entries written to the database.
    /// </returns>
    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:

                    entry.Entity.CreatedOnUtc = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "System";

                    break;

                case EntityState.Modified:

                    entry.Entity.LastModifiedOnUtc = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "System";

                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}