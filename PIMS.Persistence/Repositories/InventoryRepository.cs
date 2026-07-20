using Microsoft.EntityFrameworkCore;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Domain.Entities;
using PIMS.Persistence.Context;

namespace PIMS.Persistence.Repositories;

/// <summary>
/// Repository implementation for Inventory entity.
/// </summary>
public class InventoryRepository : Repository<Inventory>, IInventoryRepository
{
    private readonly PimsDbContext _context;


    /// <summary>
    /// Initializes a new instance of InventoryRepository.
    /// </summary>
    /// <param name="context">Database context.</param>
    public InventoryRepository(PimsDbContext context)
        : base(context)
    {
        _context = context;
    }


    /// <summary>
    /// Gets inventory record by product id.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Inventory record if found.</returns>
    public async Task<Inventory?> GetByProductIdAsync(
        int productId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Inventories
            .Include(x => x.Product)
            .FirstOrDefaultAsync(
                x => x.ProductId == productId,
                cancellationToken);
    }


    /// <summary>
    /// Gets inventory with transaction history.
    /// </summary>
    /// <param name="inventoryId">Inventory identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    /// Inventory with transactions and performed users.
    /// </returns>
    public async Task<Inventory?> GetInventoryWithTransactionsAsync(
        int inventoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Inventories
            .Include(x => x.Product)
            .Include(x => x.InventoryTransactions)
                .ThenInclude(x => x.PerformedByUser)
            .FirstOrDefaultAsync(
                x => x.Id == inventoryId,
                cancellationToken);
    }


    /// <summary>
    /// Gets products having low stock.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Low stock inventory list.</returns>
    public async Task<List<Inventory>> GetLowStockItemsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Inventories
            .Include(x => x.Product)
            .Where(x => x.Quantity <= x.LowStockThreshold)
            .ToListAsync(cancellationToken);
    }
}