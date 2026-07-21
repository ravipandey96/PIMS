using System.ComponentModel.DataAnnotations;

namespace PIMS.Application.DTOs.Authentication;

/// <summary>
/// Represents a request to obtain a new access token
/// using a valid refresh token.
/// </summary>
public sealed class RefreshTokenRequestDto
{
    /// <summary>
    /// Gets or sets the refresh token issued during login.
    /// </summary>
    [Required(ErrorMessage = "Refresh token is required.")]
    public string RefreshToken { get; set; } = string.Empty;
}