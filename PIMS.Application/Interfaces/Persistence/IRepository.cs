using System.Linq.Expressions;
using PIMS.Domain.Common;

namespace PIMS.Application.Interfaces.Persistence;

/// <summary>
/// Defines generic repository operations for all entities.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of entities.</returns>
    Task<IEnumerable<T>> GetAllAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets entities that satisfy the specified condition.
    /// </summary>
    /// <param name="predicate">Filter condition.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of matching entities.</returns>
    Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the first entity that satisfies the specified condition.
    /// </summary>
    /// <param name="predicate">Filter condition.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The matching entity if found; otherwise, null.</returns>
    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether any entity satisfies the specified condition.
    /// </summary>
    /// <param name="predicate">Filter condition.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if a matching entity exists; otherwise, false.</returns>
    Task<bool> AnyAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the total number of entities.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Total entity count.</returns>
    Task<int> CountAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the number of entities that satisfy the specified condition.
    /// </summary>
    /// <param name="predicate">Filter condition.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Matching entity count.</returns>
    Task<int> CountAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated collection of entities.
    /// </summary>
    /// <param name="pageNumber">Page number (starting from 1).</param>
    /// <param name="pageSize">Number of records per page.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated collection of entities.</returns>
    Task<IEnumerable<T>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddAsync(
        T entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds multiple entities.
    /// </summary>
    /// <param name="entities">Collection of entities.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(T entity);

    /// <summary>
    /// Updates multiple entities.
    /// </summary>
    /// <param name="entities">Collection of entities.</param>
    void UpdateRange(IEnumerable<T> entities);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(T entity);

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    /// <param name="entities">Collection of entities.</param>
    void DeleteRange(IEnumerable<T> entities);
}