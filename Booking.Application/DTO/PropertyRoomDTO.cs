namespace Booking.Application.DTO;

public class PropertyRoomDTO
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public List<string> Images { get; set; } = new();
}
