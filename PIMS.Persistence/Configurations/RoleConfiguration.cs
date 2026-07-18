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

        // Audit Fields
        builder.Property(r => r.CreatedOn)
               .IsRequired();

        builder.Property(r => r.CreatedBy);

        builder.Property(r => r.ModifiedOn);

        builder.Property(r => r.ModifiedBy);

        // Relationship
        builder.HasMany(r => r.Users)
               .WithOne(u => u.Role)
               .HasForeignKey(u => u.RoleId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}