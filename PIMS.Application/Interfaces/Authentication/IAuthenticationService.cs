using PIMS.Application.DTOs.Authentication;

namespace PIMS.Application.Interfaces.Authentication;

/// <summary>
/// Defines authentication operations for the application.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Authenticates a user using email/username and password.
    /// Generates a JWT access token and refresh token.
    /// </summary>
    /// <param name="request">
    /// Login credentials containing email/username and password.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Authentication response containing:
    /// <list type="bullet">
    /// <item>JWT access token.</item>
    /// <item>Refresh token.</item>
    /// <item>Token expiration information.</item>
    /// <item>Authenticated user information.</item>
    /// </list>
    /// </returns>
    Task<LoginResponseDto> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default);



    /// <summary>
    /// Generates a new access token using a valid refresh token.
    /// 
    /// The existing refresh token is revoked and replaced
    /// with a new refresh token using refresh token rotation.
    /// </summary>
    /// <param name="request">
    /// Refresh token request containing the existing token.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// New authentication response containing:
    /// <list type="bullet">
    /// <item>New JWT access token.</item>
    /// <item>New refresh token.</item>
    /// <item>Updated expiration information.</item>
    /// </list>
    /// </returns>
    Task<LoginResponseDto> RefreshTokenAsync(
        RefreshTokenRequestDto request,
        CancellationToken cancellationToken = default);



    /// <summary>
    /// Revokes a refresh token and terminates the user's session.
    ///
    /// The access token remains valid until its expiry because JWT tokens
    /// are stateless. Future token renewal is prevented by revoking
    /// the refresh token.
    /// </summary>
    /// <param name="request">
    /// Logout request containing refresh token.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    Task LogoutAsync(
        LogoutRequestDto request,
        CancellationToken cancellationToken = default);
}