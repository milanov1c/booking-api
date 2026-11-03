namespace Booking.Domain;

public class PropertyRoom : BaseEntity
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int PropertyId { get; set; }
    public virtual Property Property { get; set; }
    public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    public virtual ICollection<PropertyRoomPrice> PropertyRoomPrices { get; set; } = new HashSet<PropertyRoomPrice>();
    public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
}