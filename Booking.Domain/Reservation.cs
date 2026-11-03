namespace Booking.Domain;

public class Reservation : BaseEntity
{
    public int UserId { get; set; }
    public int PropertyRoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    public virtual PropertyRoom PropertyRoom { get; set; }
    public virtual User User { get; set; }
}