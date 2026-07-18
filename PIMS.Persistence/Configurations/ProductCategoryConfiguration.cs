using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.Entities;

namespace PIMS.Persistence.Configurations;

/// <summary>
/// Configures the ProductCategory entity.
/// </summary>
public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    /// <summary>
    /// Configures the ProductCategory entity.
    /// </summary>
    /// <param name="builder">Entity type builder.</param>
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        // Table Name
        builder.ToTable("ProductCategories");

        // Composite Primary Key
        builder.HasKey(pc => new
        {
            pc.ProductId,
            pc.CategoryId
        });

        // Relationship : ProductCategory -> Product
        builder.HasOne(pc => pc.Product)
               .WithMany(p => p.ProductCategories)
               .HasForeignKey(pc => pc.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relationship : ProductCategory -> Category
        builder.HasOne(pc => pc.Category)
               .WithMany(c => c.ProductCategories)
               .HasForeignKey(pc => pc.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(pc => pc.ProductId);

        builder.HasIndex(pc => pc.CategoryId);
    }
}