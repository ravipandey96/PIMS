using System;

namespace PIMS.Application.Interfaces.DateTime_
{
    /// <summary>
    /// Provides abstraction for retrieving the current system date and time.
    /// This abstraction improves testability by allowing date and time to be mocked.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the current UTC date and time.
        /// </summary>
        DateTime UtcNow { get; }

        /// <summary>
        /// Gets the current local date and time.
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Gets today's local date.
        /// </summary>
        DateOnly Today { get; }
    }
}