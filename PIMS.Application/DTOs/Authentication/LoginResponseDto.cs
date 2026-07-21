namespace PIMS.Application.DTOs.Authentication;

/// <summary>
/// Represents the response returned after successful authentication.
/// </summary>
public sealed class LoginResponseDto
{
    /// <summary>
    /// Gets or sets the JWT access token.
    /// </summary>
    public string AccessToken { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the refresh token.
    /// </summary>
    public string RefreshToken { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the access token expiration date and time (UTC).
    /// </summary>
    public DateTime AccessTokenExpiresOnUtc { get; init; }

    /// <summary>
    /// Gets or sets the refresh token expiration date and time (UTC).
    /// </summary>
    public DateTime RefreshTokenExpiresOnUtc { get; init; }

    /// <summary>
    /// Gets or sets the authenticated user's identifier.
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    /// Gets or sets the authenticated user's username.
    /// </summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the authenticated user's email address.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the authenticated user's role.
    /// </summary>
    public string Role { get; init; } = string.Empty;
}