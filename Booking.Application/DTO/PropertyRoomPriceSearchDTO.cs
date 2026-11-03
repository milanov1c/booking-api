namespace Booking.Application.DTO;

public class PropertyRoomPriceSearchDTO
{
    public int PropertyRoomId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
