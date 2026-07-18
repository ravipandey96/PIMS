namespace PIMS.Domain.Enums;

/// <summary>
/// Represents the different types of inventory transactions.
/// </summary>
public enum InventoryTransactionType
{
    /// <summary>
    /// Stock added to inventory.
    /// </summary>
    StockIn = 1,

    /// <summary>
    /// Stock removed from inventory.
    /// </summary>
    StockOut = 2,

    /// <summary>
    /// Manual inventory adjustment.
    /// </summary>
    Adjustment = 3
}