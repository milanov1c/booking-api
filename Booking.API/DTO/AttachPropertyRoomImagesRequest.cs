using Microsoft.AspNetCore.Http;

namespace Booking.API.DTO;

/// <summary>
/// Form-bindable request model for POST /api/propertyroom/attach (multipart/form-data).
/// Send multiple files under the same "Images" form key.
/// </summary>
public class AttachPropertyRoomImagesRequest
{
    public int PropertyRoomId { get; set; }
    public List<IFormFile> Images { get; set; } = new();
}
