namespace PIMS.Application.Interfaces.Logging;

/// <summary>
/// Defines operations for application logging.
/// </summary>
public interface ILoggingService
{
    /// <summary>
    /// Logs a trace message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogTrace(string message);

    /// <summary>
    /// Logs a debug message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogDebug(string message);

    /// <summary>
    /// Logs an informational message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogInformation(string message);

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogWarning(string message);

    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogError(string message);

    /// <summary>
    /// Logs an exception.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <param name="message">Additional message describing the error.</param>
    void LogError(Exception exception, string message);

    /// <summary>
    /// Logs a critical error.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogCritical(string message);

    /// <summary>
    /// Logs a critical exception.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <param name="message">Additional message describing the critical error.</param>
    void LogCritical(Exception exception, string message);
}