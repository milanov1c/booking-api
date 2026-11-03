using Booking.Application;
using Microsoft.AspNetCore.Http;

namespace Booking.API.Uploads;

/// <summary>
/// Adapts ASP.NET Core's IFormFile to the ASP.NET-agnostic IUploadedFile
/// abstraction used by the Application/Implementation layers.
/// </summary>
public class FormFileUploadedFile : IUploadedFile
{
    private readonly IFormFile _formFile;

    public FormFileUploadedFile(IFormFile formFile)
    {
        _formFile = formFile;
    }

    public string FileName => _formFile.FileName;
    public string ContentType => _formFile.ContentType;
    public long Length => _formFile.Length;

    public Stream OpenReadStream() => _formFile.OpenReadStream();
}
