using PIMS.Domain.Entities;

namespace PIMS.Application.Interfaces.Persistence;

/// <summary>
/// Defines repository operations specific to the Inventory entity.
/// </summary>
public interface IInventoryRepository : IRepository<Inventory>
{
    /// <summary>
    /// Gets the inventory record for a specific product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The inventory record if found; otherwise, null.</returns>
    Task<Inventory?> GetByProductIdAsync(
        int productId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets inventory records where the available quantity is below
    /// the configured reorder level.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of low-stock inventory records.</returns>
    Task<IEnumerable<Inventory>> GetLowStockItemsAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets inventory records that are currently out of stock.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of out-of-stock inventory records.</returns>
    Task<IEnumerable<Inventory>> GetOutOfStockItemsAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an inventory record together with its related product
    /// and transaction history.
    /// </summary>
    /// <param name="inventoryId">The inventory identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The inventory record if found; otherwise, null.</returns>
    Task<Inventory?> GetInventoryWithDetailsAsync(
        int inventoryId,
        CancellationToken cancellationToken = default);
}