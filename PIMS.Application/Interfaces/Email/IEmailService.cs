namespace PIMS.Application.Interfaces.Services;

/// <summary>
/// Defines methods for sending email messages.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email message asynchronously.
    /// </summary>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The HTML or plain text body of the email.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendEmailAsync(string to, string subject, string body);
}