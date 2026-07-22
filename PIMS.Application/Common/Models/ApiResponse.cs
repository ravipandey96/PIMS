namespace PIMS.Application.Common.Models;

/// <summary>
/// Represents a standardized successful API response.
/// </summary>
/// <typeparam name="T">
/// Type of returned data.
/// </typeparam>
public sealed class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the request succeeded.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// Response message.
    /// </summary>
    public string Message { get; init; } = string.Empty;

    /// <summary>
    /// Returned data.
    /// </summary>
    public T? Data { get; init; }

    /// <summary>
    /// UTC response timestamp.
    /// </summary>
    public DateTime TimestampUtc { get; init; }
        = DateTime.UtcNow;



    public static ApiResponse<T> Ok(
        T data,
        string message = "Request completed successfully.")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }
}