namespace PIMS.Application.Interfaces.FileStorage;

/// <summary>
/// Defines operations for storing and managing files.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Uploads a file and returns its storage path or URL.
    /// </summary>
    /// <param name="fileStream">The file content stream.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="contentType">The file content type.</param>
    /// <param name="folderName">The destination folder.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The stored file path or URL.</returns>
    Task<string> UploadFileAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        string folderName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Downloads a file as a stream.
    /// </summary>
    /// <param name="filePath">The stored file path or URL.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The file stream.</returns>
    Task<Stream> DownloadFileAsync(
        string filePath,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a stored file.
    /// </summary>
    /// <param name="filePath">The stored file path or URL.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task DeleteFileAsync(
        string filePath,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether a file exists.
    /// </summary>
    /// <param name="filePath">The stored file path or URL.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the file exists; otherwise, false.</returns>
    Task<bool> FileExistsAsync(
        string filePath,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the public URL of a stored file.
    /// </summary>
    /// <param name="filePath">The stored file path.</param>
    /// <returns>The public URL.</returns>
    string GetFileUrl(string filePath);
}