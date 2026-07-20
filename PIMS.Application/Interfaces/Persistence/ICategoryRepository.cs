using PIMS.Domain.Entities;

namespace PIMS.Application.Interfaces.Persistence;

/// <summary>
/// Defines repository operations specific to the Category entity.
/// </summary>
public interface ICategoryRepository : IRepository<Category>
{
    /// <summary>
    /// Gets a category by its name.
    /// </summary>
    /// <param name="name">The category name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The category if found; otherwise, null.</returns>
    Task<Category?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether a category with the specified name exists.
    /// </summary>
    /// <param name="name">The category name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the category exists; otherwise, false.</returns>
    Task<bool> CategoryExistsAsync(
        string name,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all active categories.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of active categories.</returns>
    Task<IEnumerable<Category>> GetActiveCategoriesAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a category together with its associated products.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The category with its products if found; otherwise, null.</returns>
    Task<Category?> GetCategoryWithProductsAsync(
        int categoryId,
        CancellationToken cancellationToken = default);
}