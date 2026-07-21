namespace PIMS.Application.Common.Exceptions;

/// <summary>
/// Represents an exception that is thrown when
/// the requested resource cannot be found.
/// </summary>
public sealed class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(
        string message,
        Exception innerException)
        : base(message, innerException)
    {
    }
}