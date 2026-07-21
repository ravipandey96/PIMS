namespace PIMS.Application.Common.Models.Authentication;

/// <summary>
/// Represents the result of generating a refresh token.
/// </summary>
public sealed class RefreshTokenResult
{
    /// <summary>
    /// Gets or sets the plain refresh token that is returned to the client.
    /// </summary>
    public string Token { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the SHA-256 hash of the refresh token
    /// that will be stored in the database.
    /// </summary>
    public string TokenHash { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the UTC expiration date and time.
    /// </summary>
    public DateTime ExpiresOnUtc { get; init; }
}