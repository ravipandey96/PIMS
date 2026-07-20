using PIMS.Domain.Entities;

namespace PIMS.Application.Interfaces.Persistence;

/// <summary>
/// Defines repository operations specific to the Product entity.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    /// <summary>
    /// Gets a product by its name.
    /// </summary>
    /// <param name="name">The product name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The product if found; otherwise, null.</returns>
    Task<Product?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether a product with the specified name exists.
    /// </summary>
    /// <param name="name">The product name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the product exists; otherwise, false.</returns>
    Task<bool> ProductExistsAsync(
        string name,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all active products.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of active products.</returns>
    Task<IEnumerable<Product>> GetActiveProductsAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all products belonging to a specific category.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of products.</returns>
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(
        int categoryId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a product together with its categories and inventory details.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The product if found; otherwise, null.</returns>
    Task<Product?> GetProductWithDetailsAsync(
        int productId,
        CancellationToken cancellationToken = default);
}