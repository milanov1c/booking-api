namespace Booking.Application.DTO;

public class PropertyDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string PropertyType { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public decimal? Rating { get; set; }
    public IEnumerable<string> Images { get; set; }
    public IEnumerable<PropertyRoomDTO> Rooms { get; set; }
    public IEnumerable<string> Amenities { get; set; }
    
}