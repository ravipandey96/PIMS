namespace PIMS.Application.Common.Models;

/// <summary>
/// Represents a standardized API error response.
/// </summary>
public sealed class ApiErrorResponse
{
    /// <summary>
    /// Indicates whether the request was successful.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// HTTP status code.
    /// </summary>
    public int StatusCode { get; init; }

    /// <summary>
    /// Error message.
    /// </summary>
    public string Message { get; init; } = string.Empty;

    /// <summary>
    /// Collection of validation or business errors.
    /// </summary>
    public IEnumerable<string>? Errors { get; init; }

    /// <summary>
    /// UTC timestamp when the error occurred.
    /// </summary>
    public DateTime TimestampUtc { get; init; } = DateTime.UtcNow;
}