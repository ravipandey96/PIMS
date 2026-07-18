using PIMS.Domain.Common;

namespace PIMS.Domain.Entities;

/// <summary>
/// Represents a user role in the Product Inventory Management System.
/// </summary>
public class Role : AuditableEntity
{
    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether the role is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Navigation property for users assigned to this role.
    /// </summary>
    public ICollection<User> Users { get; set; } = new List<User>();
}