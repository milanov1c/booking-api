namespace Booking.Domain;

public class Amenity : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<AmenityProperty> AmenityProperties { get; set; } = new HashSet<AmenityProperty>();
}