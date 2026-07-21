using PIMS.Application.Common.Models.Authentication;

namespace PIMS.Application.Interfaces.Authentication;

/// <summary>
/// Defines operations for generating and validating refresh tokens.
/// </summary>
public interface IRefreshTokenService
{
    /// <summary>
    /// Generates a new refresh token.
    /// </summary>
    /// <param name="rememberMe">
    /// Indicates whether the user selected "Remember Me".
    /// This may affect the refresh token lifetime.
    /// </param>
    /// <returns>
    /// A <see cref="RefreshTokenResult"/> containing:
    /// <list type="bullet">
    /// <item>The plain refresh token.</item>
    /// <item>The SHA-256 hash for database storage.</item>
    /// <item>The expiration date in UTC.</item>
    /// </list>
    /// </returns>
    RefreshTokenResult GenerateRefreshToken(bool rememberMe = false);

    /// <summary>
    /// Computes the SHA-256 hash of a refresh token.
    /// </summary>
    /// <param name="refreshToken">The plain refresh token.</param>
    /// <returns>The hashed refresh token.</returns>
    string ComputeHash(string refreshToken);
}