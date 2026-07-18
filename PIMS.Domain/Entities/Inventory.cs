using PIMS.Domain.Common;

namespace PIMS.Domain.Entities;

/// <summary>
/// Represents inventory information for a product.
/// </summary>
public class Inventory : AuditableEntity
{
    /// <summary>
    /// Gets or sets the related product identifier.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Navigation property to the related product.
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Gets or sets the available quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the minimum stock threshold.
    /// </summary>
    public int LowStockThreshold { get; set; }

    /// <summary>
    /// Gets or sets the warehouse location.
    /// </summary>
    public string? WarehouseLocation { get; set; }

    /// <summary>
    /// Navigation property for inventory transactions.
    /// </summary>
    public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
}