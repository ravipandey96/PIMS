using PIMS.Domain.Entities;

namespace PIMS.Application.Interfaces.Persistence;

/// <summary>
/// Defines repository operations specific to RefreshToken entity.
/// </summary>
public interface IRefreshTokenRepository
    : IRepository<RefreshToken>
{
    /// <summary>
    /// Gets a refresh token using its SHA-256 hash.
    /// </summary>
    /// <param name="tokenHash">
    /// Hashed refresh token value.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Refresh token entity if found; otherwise null.
    /// </returns>
    Task<RefreshToken?> GetByTokenHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Gets all active refresh tokens for a user.
    /// Useful for session management.
    /// </summary>
    /// <param name="userId">
    /// User identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Collection of active refresh tokens.
    /// </returns>
    Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Revokes all refresh tokens belonging to a user.
    /// Useful when password changes or account logout from all devices.
    /// </summary>
    /// <param name="userId">
    /// User identifier.
    /// </param>
    /// <param name="reason">
    /// Reason for revocation.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    Task RevokeAllUserTokensAsync(
        int userId,
        string reason,
        CancellationToken cancellationToken = default);
}