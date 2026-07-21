using PIMS.Application.Common.Models.Authentication;
using PIMS.Application.Interfaces.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace PIMS.Infrastructure.Authentication;

/// <summary>
/// Provides functionality for generating and hashing refresh tokens.
/// </summary>
public sealed class RefreshTokenService : IRefreshTokenService
{
    /// <summary>
    /// Default refresh token lifetime.
    /// </summary>
    private static readonly TimeSpan DefaultLifetime = TimeSpan.FromDays(7);

    /// <summary>
    /// Refresh token lifetime when Remember Me is selected.
    /// </summary>
    private static readonly TimeSpan RememberMeLifetime = TimeSpan.FromDays(30);

    /// <inheritdoc/>
    public RefreshTokenResult GenerateRefreshToken(bool rememberMe = false)
    {
        // Generate 64 cryptographically secure random bytes.
        byte[] randomBytes = RandomNumberGenerator.GetBytes(64);

        // Convert to Base64 string.
        string refreshToken = Convert.ToBase64String(randomBytes);

        // Hash before storing in database.
        string tokenHash = ComputeHash(refreshToken);

        // Calculate expiration.
        DateTime expiresOnUtc = DateTime.UtcNow.Add(
            rememberMe
                ? RememberMeLifetime
                : DefaultLifetime);

        return new RefreshTokenResult
        {
            Token = refreshToken,
            TokenHash = tokenHash,
            ExpiresOnUtc = expiresOnUtc
        };
    }

    /// <inheritdoc/>
    public string ComputeHash(string refreshToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(refreshToken);

        byte[] bytes = Encoding.UTF8.GetBytes(refreshToken);

        byte[] hash = SHA256.HashData(bytes);

        return Convert.ToHexString(hash);
    }
}