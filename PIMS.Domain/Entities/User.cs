using PIMS.Domain.Common;

namespace PIMS.Domain.Entities;

/// <summary>
/// Represents an application user.
/// </summary>
public class User : AuditableEntity
{
    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hashed password.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's phone number.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets whether the user account is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the related role identifier.
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Navigation property to the user's role.
    /// </summary>
    public Role Role { get; set; } = null!;

    /// <summary>
    /// Navigation property for inventory transactions performed by this user.
    /// </summary>
    public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
}