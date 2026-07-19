using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.Entities;

namespace PIMS.Persistence.Configurations;

/// <summary>
/// Configures the Role entity.
/// </summary>
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    /// <summary>
    /// Configures the Role entity.
    /// </summary>
    /// <param name="builder">Entity type builder.</param>
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Table Name
        builder.ToTable("Roles");

        // Primary Key
        builder.HasKey(r => r.Id);

        // Id
        builder.Property(r => r.Id)
               .ValueGeneratedOnAdd();

        // Name
        builder.Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(100);

        // Unique Index
        builder.HasIndex(r => r.Name)
               .IsUnique();

        // Description
        builder.Property(r => r.Description)
               .HasMaxLength(500);

        // IsActive
        builder.Property(r => r.IsActive)
               .HasDefaultValue(true);

        // =========================
        // Audit Fields
        // =========================

        builder.Property(r => r.CreatedOnUtc)
               .IsRequired();

        builder.Property(r => r.CreatedBy)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(r => r.LastModifiedOnUtc);

        builder.Property(r => r.LastModifiedBy)
               .HasMaxLength(100);

        builder.Property(r => r.IsDeleted)
               .HasDefaultValue(false);

        builder.Property(r => r.DeletedOnUtc);

        builder.Property(r => r.DeletedBy)
               .HasMaxLength(100);

        // Relationship
        builder.HasMany(r => r.Users)
               .WithOne(u => u.Role)
               .HasForeignKey(u => u.RoleId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}