using Microsoft.EntityFrameworkCore;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Domain.Entities;
using PIMS.Persistence.Context;

namespace PIMS.Persistence.Repositories;

/// <summary>
/// Repository implementation for the <see cref="Category"/> entity.
/// </summary>
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CategoryRepository(PimsDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc/>
    public async Task<Category?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .Include(c => c.ProductCategories)
                .ThenInclude(pc => pc.Product)
            .FirstOrDefaultAsync(
                c => c.Name == name,
                cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> CategoryExistsAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AnyAsync(
                c => c.Name == name,
                cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Category>> GetActiveCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .Where(c => c.IsActive)
            .Include(c => c.ProductCategories)
                .ThenInclude(pc => pc.Product)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Category?> GetCategoryWithProductsAsync(
        int categoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .Include(c => c.ProductCategories)
                .ThenInclude(pc => pc.Product)
            .FirstOrDefaultAsync(
                c => c.Id == categoryId,
                cancellationToken);
    }
}