using Booking.Application;

namespace Booking.Implementation.Storage;

/// <summary>
/// Saves uploaded files to a local folder on disk (relative to the app's
/// working/content directory) and returns a relative path that can be
/// stored in the database and served back as a static file.
/// </summary>
public class LocalFileStorage : IFileStorage
{
    private readonly string _rootPath;

    public LocalFileStorage(string rootPath = "wwwroot/uploads")
    {
        _rootPath = rootPath;
    }

    public string Save(IUploadedFile file, string subFolder)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        var targetDirectory = Path.Combine(_rootPath, subFolder);
        Directory.CreateDirectory(targetDirectory);

        var extension = Path.GetExtension(file.FileName);
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(targetDirectory, uniqueFileName);

        using (var sourceStream = file.OpenReadStream())
        using (var destinationStream = File.Create(fullPath))
        {
            sourceStream.CopyTo(destinationStream);
        }

        // Relative, web-friendly path (forward slashes) to store in the DB.
        return $"/uploads/{subFolder}/{uniqueFileName}".Replace("//", "/");
    }

    public void Delete(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            return;
        }

        // relativePath looks like "/uploads/properties/xyz.png" - strip the
        // leading "/uploads/" so it's relative to _rootPath ("wwwroot/uploads").
        var trimmed = relativePath.TrimStart('/');
        const string prefix = "uploads/";
        if (trimmed.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            trimmed = trimmed.Substring(prefix.Length);
        }

        var fullPath = Path.Combine(_rootPath, trimmed);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}
