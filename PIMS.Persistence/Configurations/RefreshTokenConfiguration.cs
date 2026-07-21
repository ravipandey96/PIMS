using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.Entities;

namespace PIMS.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for RefreshToken entity.
/// </summary>
public sealed class RefreshTokenConfiguration
    : IEntityTypeConfiguration<RefreshToken>
{
    /// <summary>
    /// Configures RefreshToken entity mapping.
    /// </summary>
    /// <param name="builder">
    /// Entity type builder.
    /// </param>
    public void Configure(
        EntityTypeBuilder<RefreshToken> builder)
    {
        /*
         * Table Configuration
         */

        builder.ToTable("RefreshTokens");



        /*
         * Primary Key
         */

        builder.HasKey(x => x.Id);



        /*
         * Token Hash Configuration
         *
         * SHA-256 hash converted to hexadecimal:
         *
         * 64 characters
         *
         * We keep 128 length to support
         * future hashing algorithm changes.
         */

        builder.Property(x => x.TokenHash)
            .IsRequired()
            .HasMaxLength(128);



        /*
         * TokenHash Index
         *
         * Every refresh request searches:
         *
         * WHERE TokenHash = xxx
         *
         * Index improves lookup performance.
         */

        builder.HasIndex(x => x.TokenHash)
            .IsUnique();



        /*
         * Expiration Configuration
         */

        builder.Property(x => x.ExpiresOnUtc)
            .IsRequired();



        /*
         * Revocation Information
         */

        builder.Property(x => x.RevocationReason)
            .HasMaxLength(255);



        /*
         * Issued Date
         */

        builder.Property(x => x.IssuedOnUtc)
            .IsRequired();



        /*
         * Relationship:
         *
         * User
         *   |
         *   |
         *   Many RefreshTokens
         *
         *
         * One user can have:
         *
         * Laptop session
         * Mobile session
         * Browser session
         */

        builder.HasOne(x => x.User)

            .WithMany(x => x.RefreshTokens)

            .HasForeignKey(x => x.UserId)

            .OnDelete(DeleteBehavior.Cascade);



        /*
         * UserId Index
         *
         * Useful for:
         *
         * - Logout from all devices
         * - Active sessions page
         * - Security audit
         */

        builder.HasIndex(x => x.UserId);
    }
}