namespace PIMS.Application.Common.Exceptions;

/// <summary>
/// Represents an exception that is thrown when
/// validation fails.
/// </summary>
public sealed class ValidationException : Exception
{
    public ValidationException()
    {
    }

    public ValidationException(string message)
        : base(message)
    {
    }

    public ValidationException(
        string message,
        Exception innerException)
        : base(message, innerException)
    {
    }
}