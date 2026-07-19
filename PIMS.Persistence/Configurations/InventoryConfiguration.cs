using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.Entities;

namespace PIMS.Persistence.Configurations;

/// <summary>
/// Configures the Inventory entity.
/// </summary>
public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    /// <summary>
    /// Configures the Inventory entity.
    /// </summary>
    /// <param name="builder">Entity type builder.</param>
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        // Table Name
        builder.ToTable("Inventories");

        // Primary Key
        builder.HasKey(i => i.Id);

        // Id
        builder.Property(i => i.Id)
               .ValueGeneratedOnAdd();

        // ProductId
        builder.Property(i => i.ProductId)
               .IsRequired();

        // Quantity
        builder.Property(i => i.Quantity)
               .IsRequired();

        // Low Stock Threshold
        builder.Property(i => i.LowStockThreshold)
               .IsRequired();

        // Warehouse Location
        builder.Property(i => i.WarehouseLocation)
               .HasMaxLength(200);

        // =========================
        // Audit Fields
        // =========================

        builder.Property(i => i.CreatedOnUtc)
               .IsRequired();

        builder.Property(i => i.CreatedBy)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(i => i.LastModifiedOnUtc);

        builder.Property(i => i.LastModifiedBy)
               .HasMaxLength(100);

        builder.Property(i => i.IsDeleted)
               .HasDefaultValue(false);

        builder.Property(i => i.DeletedOnUtc);

        builder.Property(i => i.DeletedBy)
               .HasMaxLength(100);

        // One Product -> One Inventory
        builder.HasOne(i => i.Product)
               .WithOne(p => p.Inventory)
               .HasForeignKey<Inventory>(i => i.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

        // One Inventory -> Many Inventory Transactions
        builder.HasMany(i => i.InventoryTransactions)
               .WithOne(it => it.Inventory)
               .HasForeignKey(it => it.InventoryId)
               .OnDelete(DeleteBehavior.Cascade);

        // ProductId must be unique (One-to-One)
        builder.HasIndex(i => i.ProductId)
               .IsUnique();
    }
}