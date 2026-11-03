using Microsoft.AspNetCore.Http;

namespace Booking.API.DTO;

/// <summary>
/// Form-bindable request model for POST /api/property/attach (multipart/form-data).
/// Send multiple files under the same "Images" form key.
/// </summary>
public class AttachPropertyImagesRequest
{
    public int PropertyId { get; set; }
    public List<IFormFile> Images { get; set; } = new();
}
