namespace Booking.Application.DTO;

public class CheckPropertyRoomAvailabilityDTO
{
    public int PropertyRoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
