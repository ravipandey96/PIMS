namespace PIMS.Application.Common.Exceptions;

/// <summary>
/// Represents an exception that is thrown when authentication
/// or authorization fails.
/// </summary>
public sealed class UnauthorizedException : Exception
{
    public UnauthorizedException()
    {
    }

    public UnauthorizedException(string message)
        : base(message)
    {
    }

    public UnauthorizedException(
        string message,
        Exception innerException)
        : base(message, innerException)
    {
    }
}