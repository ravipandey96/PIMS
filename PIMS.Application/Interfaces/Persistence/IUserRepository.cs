using PIMS.Domain.Entities;

namespace PIMS.Application.Interfaces.Persistence;

/// <summary>
/// Defines repository operations specific to the User entity.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Gets a user by their email address.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user by their username.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<User?> GetByUsernameAsync(
        string username,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether an email address already exists.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the email exists; otherwise, false.</returns>
    Task<bool> EmailExistsAsync(
        string email,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether a username already exists.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the username exists; otherwise, false.</returns>
    Task<bool> UsernameExistsAsync(
        string username,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all users assigned to a specific role.
    /// </summary>
    /// <param name="roleId">The role identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of users.</returns>
    Task<IEnumerable<User>> GetUsersByRoleAsync(
        int roleId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all active users.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of active users.</returns>
    Task<IEnumerable<User>> GetActiveUsersAsync(
        CancellationToken cancellationToken = default);
}