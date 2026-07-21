using System.ComponentModel.DataAnnotations;

namespace PIMS.Application.DTOs.Authentication;

/// <summary>
/// Represents a request to log out the current user.
/// </summary>
public sealed class LogoutRequestDto
{
    /// <summary>
    /// Gets or sets the refresh token that should be revoked.
    /// </summary>
    [Required(ErrorMessage = "Refresh token is required.")]
    public string RefreshToken { get; set; } = string.Empty;
}