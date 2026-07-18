namespace PIMS.Domain.Entities;

/// <summary>
/// Represents the many-to-many relationship between Product and Category.
/// </summary>
public class ProductCategory
{
    /// <summary>
    /// Gets or sets the Product identifier.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Navigation property to the Product.
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Gets or sets the Category identifier.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Navigation property to the Category.
    /// </summary>
    public Category Category { get; set; } = null!;
}