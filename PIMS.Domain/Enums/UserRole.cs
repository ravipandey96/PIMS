namespace PIMS.Domain.Enums;

/// <summary>
/// Defines the roles available in the Product Inventory Management System.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// System Administrator with full access.
    /// </summary>
    Administrator = 1,

    /// <summary>
    /// Inventory Manager responsible for stock management.
    /// </summary>
    InventoryManager = 2,

    /// <summary>
    /// Standard employee with limited access.
    /// </summary>
    Employee = 3
}