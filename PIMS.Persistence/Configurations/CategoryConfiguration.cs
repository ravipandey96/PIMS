using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.Entities;

namespace PIMS.Persistence.Configurations;

/// <summary>
/// Configures the Category entity.
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    /// <summary>
    /// Configures the Category entity.
    /// </summary>
    /// <param name="builder">Entity type builder.</param>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Table Name
        builder.ToTable("Categories");

        // Primary Key
        builder.HasKey(c => c.Id);

        // Id
        builder.Property(c => c.Id)
               .ValueGeneratedOnAdd();

        // Name
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        // Unique Index
        builder.HasIndex(c => c.Name)
               .IsUnique();

        // Description
        builder.Property(c => c.Description)
               .HasMaxLength(500);

        // Is Active
        builder.Property(c => c.IsActive)
               .HasDefaultValue(true);

        // =========================
        // Audit Fields
        // =========================

        builder.Property(c => c.CreatedOnUtc)
               .IsRequired();

        builder.Property(c => c.CreatedBy)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.LastModifiedOnUtc);

        builder.Property(c => c.LastModifiedBy)
               .HasMaxLength(100);

        builder.Property(c => c.IsDeleted)
               .HasDefaultValue(false);

        builder.Property(c => c.DeletedOnUtc);

        builder.Property(c => c.DeletedBy)
               .HasMaxLength(100);

        // Relationship : Category -> ProductCategories
        builder.HasMany(c => c.ProductCategories)
               .WithOne(pc => pc.Category)
               .HasForeignKey(pc => pc.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}