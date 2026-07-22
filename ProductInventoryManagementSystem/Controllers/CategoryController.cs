using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIMS.Application.Common.Models;
using PIMS.Application.DTOs.Category;
using PIMS.Application.Interfaces.Services;

namespace PIMS.API.Controllers;

/// <summary>
/// Provides endpoints for managing product categories.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public sealed partial class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryController"/> class.
    /// </summary>
    /// <param name="categoryService">
    /// Category service.
    /// </param>
    public CategoryController(
        ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// A collection of categories.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(
        typeof(ApiResponse<IEnumerable<CategoryDto>>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetAll(
        CancellationToken cancellationToken)
    {
        IEnumerable<CategoryDto> categories =
            await _categoryService.GetAllAsync(
                cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<CategoryDto>>.Success(
                categories,
                "Categories retrieved successfully."));
    }

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
    /// Category details.
    /// </returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(
        typeof(ApiResponse<CategoryDto>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(ApiResponse<object>),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        CategoryDto category =
            await _categoryService.GetByIdAsync(
                id,
                cancellationToken);

        return Ok(
            ApiResponse<CategoryDto>.Success(
                category,
                "Category retrieved successfully."));
    }
}
public sealed partial class CategoryController
{
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
    /// Newly created category.
    /// </returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(
        typeof(ApiResponse<CategoryDto>),
        StatusCodes.Status201Created)]
    [ProducesResponseType(
        typeof(ApiResponse<object>),
        StatusCodes.Status400BadRequest)]
    [ProducesResponseType(
        typeof(ApiResponse<object>),
        StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> Create(
        [FromBody] CreateCategoryDto request,
        CancellationToken cancellationToken)
    {
        CategoryDto category =
            await _categoryService.CreateAsync(
                request,
                cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = category.Id },
            ApiResponse<CategoryDto>.Success(
                category,
                "Category created successfully."));
    }

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
    /// Updated category.
    /// </returns>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(
        typeof(ApiResponse<CategoryDto>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(ApiResponse<object>),
        StatusCodes.Status400BadRequest)]
    [ProducesResponseType(
        typeof(ApiResponse<object>),
        StatusCodes.Status404NotFound)]
    [ProducesResponseType(
        typeof(ApiResponse<object>),
        StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<CategoryDto>>> Update(
        int id,
        [FromBody] UpdateCategoryDto request,
        CancellationToken cancellationToken)
    {
        CategoryDto category =
            await _categoryService.UpdateAsync(
                id,
                request,
                cancellationToken);

        return Ok(
            ApiResponse<CategoryDto>.Success(
                category,
                "Category updated successfully."));
    }
}
public sealed partial class CategoryController
{
    /// <summary>
    /// Deletes an existing category.
    /// </summary>
    /// <param name="id">
    /// Category identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Success response.
    /// </returns>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(
        typeof(ApiResponse<object>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(ApiResponse<object>),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await _categoryService.DeleteAsync(
            id,
            cancellationToken);

        return Ok(
            ApiResponse<object>.Success(
                null,
                "Category deleted successfully."));
    }
}