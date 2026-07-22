namespace PIMS.Application.Common.Exceptions;

/// <summary>
/// Represents an exception that is thrown when
/// validation fails.
/// </summary>
public sealed class ValidationException : Exception
{
    public ValidationException(
        string message)
        : base(message)
    {
    }

    public ValidationException(
        IEnumerable<string> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors.ToList();
    }

    /// <summary>
    /// Gets the validation errors.
    /// </summary>
    public IReadOnlyCollection<string> Errors { get; } =
        Array.Empty<string>();
}