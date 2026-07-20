namespace PIMS.Application.Interfaces.Services;

/// <summary>
/// Provides information about the currently authenticated user.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the unique identifier of the current user.
    /// </summary>
    Guid? UserId { get; }

    /// <summary>
    /// Gets the email address of the current user.
    /// </summary>
    string? Email { get; }

    /// <summary>
    /// Gets the role of the current user.
    /// </summary>
    string? Role { get; }

    /// <summary>
    /// Gets a value indicating whether the current user is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }
}