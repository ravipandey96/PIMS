namespace PIMS.Domain.Common;

/// <summary>
/// Represents a base entity with audit information.
/// </summary>
public abstract class AuditableEntity
{
    /// <summary>
    /// Gets or sets the primary key.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets when the entity was created (UTC).
    /// </summary>
    public DateTime CreatedOnUtc { get; set; }

    /// <summary>
    /// Gets or sets who created the entity.
    /// </summary>
    public string CreatedBy { get; set; } = "System";

    /// <summary>
    /// Gets or sets when the entity was last modified (UTC).
    /// </summary>
    public DateTime? LastModifiedOnUtc { get; set; }

    /// <summary>
    /// Gets or sets who last modified the entity.
    /// </summary>
    public string? LastModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets whether the entity has been soft deleted.
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Gets or sets when the entity was deleted (UTC).
    /// </summary>
    public DateTime? DeletedOnUtc { get; set; }

    /// <summary>
    /// Gets or sets who deleted the entity.
    /// </summary>
    public string? DeletedBy { get; set; }
}