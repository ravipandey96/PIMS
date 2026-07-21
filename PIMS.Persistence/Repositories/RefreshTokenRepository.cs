using Microsoft.EntityFrameworkCore;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Domain.Entities;
using PIMS.Persistence.Context;

namespace PIMS.Persistence.Repositories;

/// <summary>
/// Repository implementation for RefreshToken entity.
/// </summary>
public sealed class RefreshTokenRepository
    : Repository<RefreshToken>, IRefreshTokenRepository
{
    private readonly PimsDbContext _context;


    /// <summary>
    /// Initializes a new instance of
    /// <see cref="RefreshTokenRepository"/>.
    /// </summary>
    /// <param name="context">
    /// Database context.
    /// </param>
    public RefreshTokenRepository(
        PimsDbContext context)
        : base(context)
    {
        _context = context;
    }



    /// <inheritdoc/>
    public async Task<RefreshToken?> GetByTokenHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tokenHash);


        return await _context.RefreshTokens

            // Load user information
            // Required for generating JWT
            .Include(x => x.User)

                // Load user's role
                .ThenInclude(x => x.Role)

            .FirstOrDefaultAsync(
                x => x.TokenHash == tokenHash,
                cancellationToken);
    }




    /// <inheritdoc/>
    public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens

            .Where(x =>
                x.UserId == userId &&
                !x.IsDeleted &&
                x.RevokedOnUtc == null &&
                x.ExpiresOnUtc > DateTime.UtcNow)

            .OrderByDescending(
                x => x.IssuedOnUtc)

            .ToListAsync(
                cancellationToken);
    }





    /// <inheritdoc/>
    public async Task RevokeAllUserTokensAsync(
        int userId,
        string reason,
        CancellationToken cancellationToken = default)
    {
        List<RefreshToken> tokens =
            await _context.RefreshTokens

                .Where(x =>
                    x.UserId == userId &&
                    !x.IsDeleted &&
                    x.RevokedOnUtc == null)

                .ToListAsync(
                    cancellationToken);



        foreach (RefreshToken token in tokens)
        {
            token.RevokedOnUtc =
                DateTime.UtcNow;


            token.RevocationReason =
                reason;
        }
    }
}