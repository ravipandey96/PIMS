using PIMS.Application.Interfaces.DateTime_;
using System;

namespace PIMS.Infrastructure.Date_Time
{
    /// <summary>
    /// Provides the current system date and time.
    /// </summary>
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        /// <summary>
        /// Gets the current Coordinated Universal Time (UTC).
        /// </summary>
        public DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Gets the current local system date and time.
        /// </summary>
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// Gets today's local date.
        /// </summary>
        public DateOnly Today => DateOnly.FromDateTime(DateTime.Now);
    }
}