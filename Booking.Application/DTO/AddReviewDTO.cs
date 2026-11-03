namespace Booking.Application.DTO;

public class AddReviewDTO
{
    public int PropertyId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}