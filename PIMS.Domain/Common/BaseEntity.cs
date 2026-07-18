namespace PIMS.Domain.Common;

/// <summary>
/// Represents the base entity for all domain entities.
/// Provides the primary key shared by all entities.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public int Id { get; set; }
}