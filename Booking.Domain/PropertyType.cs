namespace Booking.Domain;

public class PropertyType : BaseEntity
{
    public string Name { get; set; }
    
    public virtual  ICollection<Property> Properties { get; set; } = new HashSet<Property>();
}