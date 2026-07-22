using System.ComponentModel.DataAnnotations;

namespace PIMS.Application.DTOs.Category;

/// <summary>
/// Represents the request used to create a new category.
/// </summary>
public sealed class CreateCategoryDto
{
    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(
        100,
        ErrorMessage = "Category name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category description.
    /// </summary>
    [StringLength(
        500,
        ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }
}