using PIMS.Domain.Common;
using PIMS.Domain.Enums;

namespace PIMS.Domain.Entities;

/// <summary>
/// Represents an inventory transaction.
/// </summary>
public class InventoryTransaction : AuditableEntity
{
    /// <summary>
    /// Gets or sets the inventory identifier.
    /// </summary>
    public int InventoryId { get; set; }

    /// <summary>
    /// Navigation property to inventory.
    /// </summary>
    public Inventory Inventory { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who performed the transaction.
    /// </summary>
    public int PerformedByUserId { get; set; }

    /// <summary>
    /// Navigation property to the user.
    /// </summary>
    public User PerformedByUser { get; set; } = null!;

    /// <summary>
    /// Gets or sets the transaction type.
    /// </summary>
    public InventoryTransactionType TransactionType { get; set; }

    /// <summary>
    /// Gets or sets the transaction quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the reason for the transaction.
    /// </summary>
    public string? Reason { get; set; }
}