using PIMS.Domain.Entities;

namespace PIMS.Application.Interfaces.Persistence;

/// <summary>
/// Defines repository operations specific to the Inventory entity.
/// </summary>
public interface IInventoryRepository : IRepository<Inventory>
{
    Task<Inventory?> GetByProductIdAsync(
        int productId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Inventory>> GetLowStockItemsAsync(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Inventory>> GetOutOfStockItemsAsync(
        CancellationToken cancellationToken = default);

    Task<Inventory?> GetInventoryWithDetailsAsync(
        int inventoryId,
        CancellationToken cancellationToken = default);
}