namespace Booking.Domain;

public class Property : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    
    public int PropertyTypeId { get; set; }
    public int DestinationId { get; set; }
    public int OwnerId { get; set; }
    public virtual Destination Destination { get; set; }
    public virtual PropertyType PropertyType { get; set; }
    public virtual User Owner { get; set; }
    
    public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    public virtual ICollection<PropertyRoom> PropertyRooms { get; set; } = new HashSet<PropertyRoom>();
    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    public virtual ICollection<AmenityProperty> AmenityProperties { get; set; } = new HashSet<AmenityProperty>();

}