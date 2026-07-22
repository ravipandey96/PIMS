namespace PIMS.Application.Common.Models;

/// <summary>
/// Represents a standardized API response.
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

    /// <summary>
    /// Creates a successful response.
    /// </summary>
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

    /// <summary>
    /// Creates a response for a newly created resource.
    /// </summary>
    public static ApiResponse<T> Created(
        T data,
        string message = "Resource created successfully.")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// Creates a failed response.
    /// </summary>
    public static ApiResponse<T> Failure(
        string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Data = default
        };
    }
}