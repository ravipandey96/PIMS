namespace PIMS.Application.Interfaces.Security;

/// <summary>
/// Defines methods for hashing and verifying user passwords.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Generates a secure hash for the specified password.
    /// </summary>
    /// <param name="password">The plain text password.</param>
    /// <returns>The hashed password.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies whether the specified password matches the stored hash.
    /// </summary>
    /// <param name="password">The plain text password.</param>
    /// <param name="passwordHash">The stored password hash.</param>
    /// <returns>
    /// <c>true</c> if the password is valid; otherwise, <c>false</c>.
    /// </returns>
    bool VerifyPassword(string password, string passwordHash);
}