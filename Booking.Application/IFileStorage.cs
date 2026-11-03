namespace Booking.Application;

public interface IFileStorage
{
    /// <summary>
    /// Saves the given file under the specified sub-folder and returns
    /// the relative path that should be persisted (e.g. in Attachment.Path).
    /// </summary>
    public string Save(IUploadedFile file, string subFolder);

    /// <summary>
    /// Deletes a previously saved file given the relative path that was
    /// returned by Save (e.g. the value stored in Attachment.Path). Should
    /// be a no-op (not throw) if the file no longer exists on disk.
    /// </summary>
    public void Delete(string relativePath);
}
