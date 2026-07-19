using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.Entities;

namespace PIMS.Persistence.Configurations;

/// <summary>
/// Configures the User entity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the User entity.
    /// </summary>
    /// <param name="builder">Entity type builder.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table Name
        builder.ToTable("Users");

        // Primary Key
        builder.HasKey(u => u.Id);

        // Id
        builder.Property(u => u.Id)
               .ValueGeneratedOnAdd();

        // First Name
        builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        // Last Name
        builder.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(100);

        // Email
        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(255);

        // Unique Email
        builder.HasIndex(u => u.Email)
               .IsUnique();

        // Password Hash
        builder.Property(u => u.PasswordHash)
               .IsRequired()
               .HasMaxLength(500);

        // Phone Number
        builder.Property(u => u.PhoneNumber)
               .HasMaxLength(20);

        // Is Active
        builder.Property(u => u.IsActive)
               .HasDefaultValue(true);

        // =========================
        // Audit Fields
        // =========================

        builder.Property(u => u.CreatedOnUtc)
               .IsRequired();

        builder.Property(u => u.CreatedBy)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.LastModifiedOnUtc);

        builder.Property(u => u.LastModifiedBy)
               .HasMaxLength(100);

        builder.Property(u => u.IsDeleted)
               .HasDefaultValue(false);

        builder.Property(u => u.DeletedOnUtc);

        builder.Property(u => u.DeletedBy)
               .HasMaxLength(100);

        // Relationship : User -> Role
        builder.HasOne(u => u.Role)
               .WithMany(r => r.Users)
               .HasForeignKey(u => u.RoleId)
               .OnDelete(DeleteBehavior.Restrict);

        // Relationship : User -> InventoryTransactions
        builder.HasMany(u => u.InventoryTransactions)
               .WithOne(it => it.PerformedByUser)
               .HasForeignKey(it => it.PerformedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}