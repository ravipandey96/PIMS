using Microsoft.EntityFrameworkCore.Storage;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Persistence.Context;

namespace PIMS.Persistence.Repositories;

/// <summary>
/// Coordinates repository operations and manages database transactions.
/// </summary>
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly PimsDbContext _context;

    private IDbContextTransaction? _transaction;

    /// <summary>
    /// Initializes a new instance of UnitOfWork.
    /// </summary>
    /// <param name="context">
    /// Database context.
    /// </param>
    public UnitOfWork(PimsDbContext context)
    {
        _context = context;

        Users = new UserRepository(_context);
        Roles = new RoleRepository(_context);
        Products = new ProductRepository(_context);
        Categories = new CategoryRepository(_context);
        Inventories = new InventoryRepository(_context);
        RefreshTokens = new RefreshTokenRepository(_context);
    }

    /// <inheritdoc/>
    public IUserRepository Users { get; }

    /// <inheritdoc/>
    public IRoleRepository Roles { get; }

    /// <inheritdoc/>
    public IProductRepository Products { get; }

    /// <inheritdoc/>
    public ICategoryRepository Categories { get; }

    /// <inheritdoc/>
    public IInventoryRepository Inventories { get; }

    /// <inheritdoc/>
    public IRefreshTokenRepository RefreshTokens { get; }

    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task BeginTransactionAsync(
        CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            return;
        }

        _transaction =
            await _context.Database.BeginTransactionAsync(
                cancellationToken);
    }

    /// <inheritdoc/>
    public async Task CommitTransactionAsync(
        CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            return;
        }

        try
        {
            await _context.SaveChangesAsync(cancellationToken);

            await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await _transaction.RollbackAsync(cancellationToken);

            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();

            _transaction = null;
        }
    }

    /// <inheritdoc/>
    public async Task RollbackTransactionAsync(
        CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            return;
        }

        try
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();

            _transaction = null;
        }
    }

    /// <summary>
    /// Releases database resources.
    /// </summary>
    public void Dispose()
    {
        _transaction?.Dispose();

        _context.Dispose();

        GC.SuppressFinalize(this);
    }
}