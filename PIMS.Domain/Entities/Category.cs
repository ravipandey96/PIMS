using PIMS.Domain.Common;

namespace PIMS.Domain.Entities;

/// <summary>
/// Represents a product category.
/// </summary>
public class Category : AuditableEntity
{
    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets whether the category is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Navigation property for products associated with this category.
    /// </summary>
    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}