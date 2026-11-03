using Microsoft.AspNetCore.Http;

namespace Booking.API.DTO;

/// <summary>
/// Form-bindable request model for POST /api/propertyroom (multipart/form-data).
/// Mapped into Booking.Application.DTO.AddPropertyRoomDTO by the controller.
/// </summary>
public class AddPropertyRoomRequest
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int PropertyId { get; set; }
    public IFormFile Image { get; set; }
}
