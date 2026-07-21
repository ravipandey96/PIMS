namespace PIMS.Application.Interfaces.Persistence;

/// <summary>
/// Represents the Unit of Work pattern for coordinating repository operations
/// and managing database transactions.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the user repository.
    /// </summary>
    IUserRepository Users { get; }

    /// <summary>
    /// Gets the role repository.
    /// </summary>
    IRoleRepository Roles { get; }

    /// <summary>
    /// Gets the product repository.
    /// </summary>
    IProductRepository Products { get; }

    /// <summary>
    /// Gets the category repository.
    /// </summary>
    ICategoryRepository Categories { get; }

    /// <summary>
    /// Gets the inventory repository.
    /// </summary>
    IInventoryRepository Inventories { get; }

    /// <summary>
    /// Commits all pending changes to the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task BeginTransactionAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current database transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task CommitTransactionAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the current database transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RollbackTransactionAsync(
        CancellationToken cancellationToken = default);
    IRefreshTokenRepository RefreshTokens { get; }
}