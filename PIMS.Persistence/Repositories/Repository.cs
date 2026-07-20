using Microsoft.EntityFrameworkCore;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Domain.Common;
using PIMS.Persistence.Context;
using System.Linq.Expressions;

namespace PIMS.Persistence.Repositories;

/// <summary>
/// Generic repository implementation.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public class Repository<T> : IRepository<T>
    where T : BaseEntity
{
    protected readonly PimsDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(PimsDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <inheritdoc/>
    public virtual async Task<T?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<T>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<int> CountAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<int> CountAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(predicate, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<T>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task AddAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task AddRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    /// <inheritdoc/>
    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    /// <inheritdoc/>
    public virtual void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    /// <inheritdoc/>
    public virtual void DeleteRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }
}