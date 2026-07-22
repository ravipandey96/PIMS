using PIMS.Application.Common.Exceptions;
using PIMS.Application.DTOs.Category;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Application.Interfaces.Services;
using PIMS.Domain.Entities;

namespace PIMS.Application.Services;


/// <summary>
/// Provides helper methods for category operations.
/// </summary>
public sealed partial class CategoryService
{
    /// <summary>
    /// Maps a <see cref="Category"/> entity to a <see cref="CategoryDto"/>.
    /// </summary>
    /// <param name="category">
    /// Category entity.
    /// </param>
    /// <returns>
    /// Category DTO.
    /// </returns>
    private static CategoryDto MapToDto(
        Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive
        };
    }

    /// <summary>
    /// Retrieves a category by its identifier.
    /// Throws an exception when the category does not exist.
    /// </summary>
    /// <param name="id">
    /// Category identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Category entity.
    /// </returns>
    /// <exception cref="NotFoundException">
    /// Thrown when the category is not found.
    /// </exception>
    private async Task<Category> GetCategoryOrThrowAsync(
        int id,
        CancellationToken cancellationToken)
    {
        Category? category =
            await _unitOfWork.Categories.GetByIdAsync(
                id,
                cancellationToken);

        if (category is null)
        {
            throw new NotFoundException(
                $"Category with Id '{id}' was not found.");
        }

        return category;
    }

    /// <summary>
    /// Validates whether a category name already exists.
    /// </summary>
    /// <param name="name">
    /// Category name.
    /// </param>
    /// <param name="currentCategoryId">
    /// Current category identifier during update.
    /// Null for create.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <exception cref="ValidationException">
    /// Thrown when a duplicate category name exists.
    /// </exception>
    private async Task ValidateCategoryNameAsync(
        string name,
        int? currentCategoryId,
        CancellationToken cancellationToken)
    {
        Category? existingCategory =
            await _unitOfWork.Categories.GetByNameAsync(
                name.Trim(),
                cancellationToken);

        if (existingCategory is null)
        {
            return;
        }

        // Allow updating the same category
        if (currentCategoryId.HasValue &&
            existingCategory.Id == currentCategoryId.Value)
        {
            return;
        }

        throw new ValidationException(
            $"Category '{name}' already exists.");
    }
}


/// <summary>
/// Provides business operations for managing categories.
/// </summary>
public sealed partial class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="unitOfWork">
    /// Unit of Work.
    /// </param>
    public CategoryService(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CategoryDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        IEnumerable<Domain.Entities.Category> categories =
            await _unitOfWork.Categories
                .GetAllAsync(cancellationToken);

        return categories
            .Select(MapToDto)
            .OrderBy(c => c.Name);
    }

    /// <inheritdoc/>
    public async Task<CategoryDto> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        Domain.Entities.Category? category =
            await _unitOfWork.Categories
                .GetByIdAsync(id, cancellationToken);

        if (category is null)
        {
            throw new NotFoundException(
                $"Category with Id '{id}' was not found.");
        }

        return MapToDto(category);
    }
}
/// <summary>
/// Provides create, update and delete operations for categories.
/// </summary>
public sealed partial class CategoryService
{
    /// <inheritdoc/>
    public async Task<CategoryDto> CreateAsync(
        CreateCategoryDto request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        await ValidateCategoryNameAsync(
            request.Name,
            null,
            cancellationToken);

        Category category = new()
        {
            Name = request.Name.Trim(),
            Description = string.IsNullOrWhiteSpace(request.Description)
                ? null
                : request.Description.Trim(),

            IsActive = true,

            // These values can later be populated from the
            // authenticated user when audit support is added.
            CreatedOnUtc = DateTime.UtcNow,
            CreatedBy = "System"
        };

        await _unitOfWork.Categories.AddAsync(
            category,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return MapToDto(category);
    }

    /// <inheritdoc/>
    public async Task<CategoryDto> UpdateAsync(
        int id,
        UpdateCategoryDto request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        Category category =
            await GetCategoryOrThrowAsync(
                id,
                cancellationToken);

        await ValidateCategoryNameAsync(
            request.Name,
            id,
            cancellationToken);

        category.Name = request.Name.Trim();

        category.Description =
            string.IsNullOrWhiteSpace(request.Description)
                ? null
                : request.Description.Trim();

        category.IsActive = request.IsActive;

        category.LastModifiedOnUtc = DateTime.UtcNow;
        category.LastModifiedBy = "System";

        _unitOfWork.Categories.Update(category);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return MapToDto(category);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        Category category =
            await GetCategoryOrThrowAsync(
                id,
                cancellationToken);

        // ----------------------------------------------------
        // Soft Delete
        // ----------------------------------------------------

        category.IsDeleted = true;
        category.DeletedOnUtc = DateTime.UtcNow;
        category.DeletedBy = "System";

        _unitOfWork.Categories.Update(category);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        // ----------------------------------------------------
        // If your project later requires hard delete,
        // simply replace the above with:
        //
        // _unitOfWork.Categories.Delete(category);
        // await _unitOfWork.SaveChangesAsync(cancellationToken);
        // ----------------------------------------------------
    }
}
