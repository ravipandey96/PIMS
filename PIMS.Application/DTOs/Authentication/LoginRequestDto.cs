using System.ComponentModel.DataAnnotations;

namespace PIMS.Application.DTOs.Authentication;

/// <summary>
/// Represents the request used to authenticate a user.
/// </summary>
public sealed class LoginRequestDto
{
    /// <summary>
    /// Gets or sets the user's email address or username.
    /// </summary>
    [Required(ErrorMessage = "Email or Username is required.")]
    [MaxLength(255)]
    public string EmailOrUsername { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the user wants to remain signed in.
    /// If true, a longer refresh token lifetime may be issued.
    /// </summary>
    public bool RememberMe { get; set; } = false;
}