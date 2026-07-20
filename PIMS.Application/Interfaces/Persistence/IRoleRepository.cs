using PIMS.Domain.Entities;

namespace PIMS.Application.Interfaces.Persistence;

/// <summary>
/// Defines repository operations specific to the Role entity.
/// </summary>
public interface IRoleRepository : IRepository<Role>
{
    /// <summary>
    /// Gets a role by its name.
    /// </summary>
    /// <param name="roleName">The role name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The role if found; otherwise, null.</returns>
    Task<Role?> GetByNameAsync(
        string roleName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether a role with the specified name exists.
    /// </summary>
    /// <param name="roleName">The role name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the role exists; otherwise, false.</returns>
    Task<bool> RoleExistsAsync(
        string roleName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a role along with its associated users.
    /// </summary>
    /// <param name="roleId">The role identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The role with its users if found; otherwise, null.</returns>
    Task<Role?> GetRoleWithUsersAsync(
        int roleId,
        CancellationToken cancellationToken = default);
}