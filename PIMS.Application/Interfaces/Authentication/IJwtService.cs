using System.Security.Claims;

namespace PIMS.Application.Interfaces.Authentication;

/// <summary>
/// Defines methods for generating and validating JSON Web Tokens (JWT).
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Generates a JWT access token for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="email">The user's email address.</param>
    /// <param name="role">The user's role.</param>
    /// <returns>The generated JWT access token.</returns>
    string GenerateToken(
        int userId,
        string email,
        string role);

    /// <summary>
    /// Validates the specified JWT access token.
    /// </summary>
    /// <param name="token">The JWT access token.</param>
    /// <returns>
    /// A <see cref="ClaimsPrincipal"/> representing the authenticated user
    /// if the token is valid; otherwise, <c>null</c>.
    /// </returns>
    ClaimsPrincipal? ValidateToken(string token);
}