using Microsoft.AspNetCore.Identity;
using PIMS.Application.Interfaces.Authentication;

namespace PIMS.Infrastructure.Authentication;

/// <summary>
/// Provides secure password hashing and verification
/// using ASP.NET Core Identity's PBKDF2 implementation.
/// </summary>
public sealed class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<object> _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordHasher"/> class.
    /// </summary>
    public PasswordHasher()
    {
        _passwordHasher = new PasswordHasher<object>();
    }

    /// <inheritdoc/>
    public string HashPassword(string password)
    {
        ArgumentNullException.ThrowIfNull(password);

        return _passwordHasher.HashPassword(null!, password);
    }

    /// <inheritdoc/>
    public bool VerifyPassword(string password, string passwordHash)
    {
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(passwordHash);

        var result = _passwordHasher.VerifyHashedPassword(
            null!,
            passwordHash,
            password);

        return result == PasswordVerificationResult.Success ||
               result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}