using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.Entities;

namespace PIMS.Persistence.Configurations;

/// <summary>
/// Configures the Product entity.
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    /// <summary>
    /// Configures the Product entity.
    /// </summary>
    /// <param name="builder">Entity type builder.</param>
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Table Name
        builder.ToTable("Products");

        // Primary Key
        builder.HasKey(p => p.Id);

        // Id
        builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();

        // Name
        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        // SKU
        builder.Property(p => p.SKU)
               .IsRequired()
               .HasMaxLength(50);

        // Unique SKU
        builder.HasIndex(p => p.SKU)
               .IsUnique();

        // Description
        builder.Property(p => p.Description)
               .HasMaxLength(1000);

        // Price
        builder.Property(p => p.Price)
               .HasPrecision(18, 2)
               .IsRequired();

        // Is Active
        builder.Property(p => p.IsActive)
               .HasDefaultValue(true);

        // Audit Fields
        builder.Property(p => p.CreatedOn)
               .IsRequired();

        builder.Property(p => p.CreatedBy);

        builder.Property(p => p.ModifiedOn);

        builder.Property(p => p.ModifiedBy);

        // Relationship : Product -> ProductCategories
        builder.HasMany(p => p.ProductCategories)
               .WithOne(pc => pc.Product)
               .HasForeignKey(pc => pc.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relationship : Product -> Inventory (One-to-One)
        builder.HasOne(p => p.Inventory)
               .WithOne(i => i.Product)
               .HasForeignKey<Inventory>(i => i.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}