namespace PIMS.Application.Interfaces.Cache;

/// <summary>
/// Defines operations for interacting with the application's cache.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Retrieves a cached value by its key.
    /// </summary>
    /// <typeparam name="T">The type of the cached value.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The cached value if found; otherwise, default.</returns>
    Task<T?> GetAsync<T>(
        string key,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores a value in the cache for the specified duration.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="value">The value to cache.</param>
    /// <param name="expiration">The cache expiration time.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SetAsync<T>(
        string key,
        T value,
        TimeSpan expiration,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a cached value.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RemoveAsync(
        string key,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether the specified cache key exists.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the key exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(
        string key,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes all cached values matching the specified pattern.
    /// </summary>
    /// <param name="pattern">The cache key pattern.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RemoveByPatternAsync(
        string pattern,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Refreshes the expiration time of an existing cache entry.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RefreshAsync(
        string key,
        CancellationToken cancellationToken = default);
}