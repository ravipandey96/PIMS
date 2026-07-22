namespace PIMS.Application.DTOs.Category;

/// <summary>
/// Represents a category returned to the client.
/// </summary>
public sealed class CategoryDto
{
    /// <summary>
    /// Gets or sets the category identifier.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the category description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets whether the category is active.
    /// </summary>
    public bool IsActive { get; init; }
}