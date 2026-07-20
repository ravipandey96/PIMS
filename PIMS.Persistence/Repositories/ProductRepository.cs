using Microsoft.EntityFrameworkCore;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Domain.Entities;
using PIMS.Persistence.Context;

namespace PIMS.Persistence.Repositories;

/// <summary>
/// Repository implementation for the Product entity.
/// </summary>
public class ProductRepository : Repository<Product>, IProductRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class.
    /// </summary>
    /// <param name="context">Database context.</param>
    public ProductRepository(PimsDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc/>
    public async Task<Product?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
            .Include(p => p.Inventory)
            .FirstOrDefaultAsync(
                p => p.Name == name,
                cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ProductExistsAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AnyAsync(
                p => p.Name == name,
                cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>> GetActiveProductsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => p.IsActive)
            .Include(p => p.Inventory)
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(
        int categoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
            .Include(p => p.Inventory)
            .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Product?> GetProductWithDetailsAsync(
        int productId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(p => p.Inventory)
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(
                p => p.Id == productId,
                cancellationToken);
    }
}