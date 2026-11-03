namespace Booking.Application.DTO;

public class UpdatePropertyDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public int PropertyTypeId { get; set; }
    public int DestinationId { get; set; }
}
