using PIMS.Domain.Common;

namespace PIMS.Domain.Entities;

/// <summary>
/// Represents a refresh token issued to a user.
/// </summary>
public class RefreshToken : AuditableEntity
{
    /// <summary>
    /// Gets or sets the SHA-256 hash of the refresh token.
    /// </summary>
    public string TokenHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets when the refresh token expires (UTC).
    /// </summary>
    public DateTime ExpiresOnUtc { get; set; }

    /// <summary>
    /// Gets or sets when the refresh token was revoked (UTC).
    /// Null if the token has not been revoked.
    /// </summary>
    public DateTime? RevokedOnUtc { get; set; }

    /// <summary>
    /// Gets or sets the hash of the refresh token that replaced this token.
    /// Used for refresh token rotation.
    /// </summary>
    public string? ReplacedByTokenHash { get; set; }

    /// <summary>
    /// Gets or sets the associated user identifier.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Navigation property to the associated user.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Gets a value indicating whether the refresh token has expired.
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= ExpiresOnUtc;

    /// <summary>
    /// Gets a value indicating whether the refresh token has been revoked.
    /// </summary>
    public bool IsRevoked => RevokedOnUtc.HasValue;

    /// <summary>
    /// Gets a value indicating whether the refresh token is currently active.
    /// </summary>
    public bool IsActive => !IsDeleted && !IsExpired && !IsRevoked;

    /// <summary>
    /// Gets or sets when the refresh token was created (UTC).
    /// </summary>
    public DateTime IssuedOnUtc { get; set; }
    /// <summary>
    /// Gets or sets why the refresh token was revoked.
    /// </summary>
    public string? RevocationReason { get; set; }
}