using PIMS.Domain.Common;

namespace PIMS.Domain.Entities;

/// <summary>
/// Represents a product in the Product Inventory Management System.
/// </summary>
public class Product : AuditableEntity
{
    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Stock Keeping Unit (SKU).
    /// Must be unique.
    /// </summary>
    public string SKU { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the selling price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets whether the product is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Navigation property for product categories.
    /// </summary>
    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    /// <summary>
    /// Navigation property for inventory.
    /// One product has one inventory record.
    /// </summary>
    public Inventory? Inventory { get; set; }
}