using System.Runtime.InteropServices.JavaScript;

namespace Booking.Application.DTO;

public class AttachPriceDTO
{
    public int PropertyRoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Price { get; set; }
}