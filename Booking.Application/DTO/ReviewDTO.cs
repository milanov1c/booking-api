namespace Booking.Application.DTO;

public class ReviewDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserFullName { get; set; }
    public int PropertyId { get; set; }
    public string PropertyName { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
