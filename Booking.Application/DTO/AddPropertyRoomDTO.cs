namespace Booking.Application.DTO;

public class AddPropertyRoomDTO
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int PropertyId { get; set; }
    public IUploadedFile Image { get; set; }
    
}