namespace Booking.Domain;

public class PropertyRoomPrice : BaseEntity
{
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
    
    public int PropertyRoomId { get; set; }
    public virtual PropertyRoom PropertyRoom { get; set; }
}