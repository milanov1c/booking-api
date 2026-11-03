namespace Booking.Domain;

public class Attachment : BaseEntity
{
    public string Path { get; set; }
    public int? PropertyId { get; set; }
    public virtual Property? Property { get; set; }
    public int? PropertyRoomId { get; set; }
    
    public virtual PropertyRoom? PropertyRoom { get; set; }
}