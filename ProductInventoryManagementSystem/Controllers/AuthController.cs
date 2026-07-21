using Microsoft.AspNetCore.Mvc;
using PIMS.Application.DTOs.Authentication;
using PIMS.Application.Interfaces.Authentication;

namespace PIMS.API.Controllers;

/// <summary>
/// Handles authentication related operations:
/// login, refresh token and logout.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;


    /// <summary>
    /// Initializes a new instance of AuthController.
    /// </summary>
    /// <param name="authenticationService">
    /// Authentication service.
    /// </param>
    public AuthController(
        IAuthenticationService authenticationService)
    {
        _authenticationService =
            authenticationService;
    }



    /// <summary>
    /// Authenticates a user and returns JWT
    /// access token and refresh token.
    /// </summary>
    /// <param name="request">
    /// Login credentials.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Login response containing tokens.
    /// </returns>
    [HttpPost("login")]
    [ProducesResponseType(
        typeof(LoginResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await _authenticationService
                .LoginAsync(
                    request,
                    cancellationToken);


        return Ok(response);
    }




    /// <summary>
    /// Generates a new access token
    /// using refresh token.
    /// </summary>
    /// <param name="request">
    /// Refresh token request.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// New access token and refresh token.
    /// </returns>
    [HttpPost("refresh-token")]
    [ProducesResponseType(
        typeof(LoginResponseDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenRequestDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await _authenticationService
                .RefreshTokenAsync(
                    request,
                    cancellationToken);


        return Ok(response);
    }




    /// <summary>
    /// Revokes the current refresh token.
    /// </summary>
    /// <param name="request">
    /// Logout request containing refresh token.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Logout result.
    /// </returns>
    [HttpPost("logout")]
    [ProducesResponseType(
        StatusCodes.Status204NoContent)]
    [ProducesResponseType(
        StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout(
        [FromBody] LogoutRequestDto request,
        CancellationToken cancellationToken)
    {
        await _authenticationService
            .LogoutAsync(
                request,
                cancellationToken);


        return NoContent();
    }
}