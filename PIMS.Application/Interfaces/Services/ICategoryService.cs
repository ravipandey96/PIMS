using PIMS.Application.DTOs.Category;

namespace PIMS.Application.Interfaces.Services;

/// <summary>
/// Defines business operations for managing categories.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// A collection of category DTOs.
    /// </returns>
    Task<IEnumerable<CategoryDto>> GetAllAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a category by its identifier.
    /// </summary>
    /// <param name="id">
    /// Category identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The category if found.
    /// </returns>
    /// <exception cref="PIMS.Application.Common.Exceptions.NotFoundException">
    /// Thrown when the category does not exist.
    /// </exception>
    Task<CategoryDto> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="request">
    /// Category creation request.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The newly created category.
    /// </returns>
    /// <exception cref="PIMS.Application.Common.Exceptions.ValidationException">
    /// Thrown when a category with the same name already exists.
    /// </exception>
    Task<CategoryDto> CreateAsync(
        CreateCategoryDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="id">
    /// Category identifier.
    /// </param>
    /// <param name="request">
    /// Updated category information.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The updated category.
    /// </returns>
    /// <exception cref="PIMS.Application.Common.Exceptions.NotFoundException">
    /// Thrown when the category does not exist.
    /// </exception>
    /// <exception cref="PIMS.Application.Common.Exceptions.ValidationException">
    /// Thrown when another category with the same name already exists.
    /// </exception>
    Task<CategoryDto> UpdateAsync(
        int id,
        UpdateCategoryDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a category.
    /// </summary>
    /// <param name="id">
    /// Category identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    /// <exception cref="PIMS.Application.Common.Exceptions.NotFoundException">
    /// Thrown when the category does not exist.
    /// </exception>
    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}