using Microsoft.AspNetCore.Http;

namespace Booking.API.DTO;

/// <summary>
/// Form-bindable request model for POST /api/property (multipart/form-data).
/// Mapped into Booking.Application.DTO.AddPropertyDTO by the controller.
/// </summary>
public class AddPropertyRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public int PropertyTypeId { get; set; }
    public int DestinationId { get; set; }
    public int LandlordId { get; set; }
    public IFormFile Image { get; set; }
}
