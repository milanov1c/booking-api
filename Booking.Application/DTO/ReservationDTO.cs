namespace Booking.Application.DTO;

public class ReservationDTO
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string PropertyName { get; set; }
    public int PropertyRoomId { get; set; }
    public string PropertyRoomName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
}
