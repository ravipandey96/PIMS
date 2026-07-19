using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.Entities;

namespace PIMS.Persistence.Configurations;

/// <summary>
/// Configures the InventoryTransaction entity.
/// </summary>
public class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
{
    /// <summary>
    /// Configures the InventoryTransaction entity.
    /// </summary>
    /// <param name="builder">Entity type builder.</param>
    public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
    {
        // Table Name
        builder.ToTable("InventoryTransactions");

        // Primary Key
        builder.HasKey(it => it.Id);

        // Id
        builder.Property(it => it.Id)
               .ValueGeneratedOnAdd();

        // InventoryId
        builder.Property(it => it.InventoryId)
               .IsRequired();

        // PerformedByUserId
        builder.Property(it => it.PerformedByUserId)
               .IsRequired();

        // Transaction Type
        builder.Property(it => it.TransactionType)
               .IsRequired();

        // Quantity
        builder.Property(it => it.Quantity)
               .IsRequired();

        // Reason
        builder.Property(it => it.Reason)
               .HasMaxLength(500);

        // =========================
        // Audit Fields
        // =========================

        builder.Property(it => it.CreatedOnUtc)
               .IsRequired();

        builder.Property(it => it.CreatedBy)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(it => it.LastModifiedOnUtc);

        builder.Property(it => it.LastModifiedBy)
               .HasMaxLength(100);

        builder.Property(it => it.IsDeleted)
               .HasDefaultValue(false);

        builder.Property(it => it.DeletedOnUtc);

        builder.Property(it => it.DeletedBy)
               .HasMaxLength(100);

        // Relationship : InventoryTransaction -> Inventory
        builder.HasOne(it => it.Inventory)
               .WithMany(i => i.InventoryTransactions)
               .HasForeignKey(it => it.InventoryId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relationship : InventoryTransaction -> User
        builder.HasOne(it => it.PerformedByUser)
               .WithMany(u => u.InventoryTransactions)
               .HasForeignKey(it => it.PerformedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(it => it.InventoryId);

        builder.HasIndex(it => it.PerformedByUserId);

        builder.HasIndex(it => it.TransactionType);

        builder.HasIndex(it => it.CreatedOnUtc);
    }
}