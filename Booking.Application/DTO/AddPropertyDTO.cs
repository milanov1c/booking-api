using Booking.Application;

namespace Booking.Application.DTO;

public class AddPropertyDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public int PropertyTypeId { get; set; }
    public int DestinationId { get; set; }
    public int LandlordId { get; set; }
    public IUploadedFile Image { get; set; }
}