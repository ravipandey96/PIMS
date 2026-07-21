using Microsoft.EntityFrameworkCore;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Domain.Entities;
using PIMS.Persistence.Context;

namespace PIMS.Persistence.Repositories;

public class InventoryRepository : Repository<Inventory>, IInventoryRepository
{
    private readonly PimsDbContext _context;

    public InventoryRepository(PimsDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<Inventory?> GetByProductIdAsync(
        int productId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Inventories
            .Include(i => i.Product)
            .FirstOrDefaultAsync(
                i => i.ProductId == productId,
                cancellationToken);
    }

    public async Task<Inventory?> GetInventoryWithDetailsAsync(
        int inventoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Inventories
            .Include(i => i.Product)
            .Include(i => i.InventoryTransactions)
                .ThenInclude(t => t.PerformedByUser)
            .FirstOrDefaultAsync(
                i => i.Id == inventoryId,
                cancellationToken);
    }

    public async Task<IEnumerable<Inventory>> GetLowStockItemsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Inventories
            .Include(i => i.Product)
            .Where(i => i.Quantity <= i.LowStockThreshold)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Inventory>> GetOutOfStockItemsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Inventories
            .Include(i => i.Product)
            .Where(i => i.Quantity == 0)
            .ToListAsync(cancellationToken);
    }
}