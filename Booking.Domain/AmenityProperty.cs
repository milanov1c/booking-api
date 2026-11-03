namespace Booking.Domain;

public class AmenityProperty
{
    public int AmenityId { get; set; }
    public int PropertyId { get; set; }
    public virtual Amenity Amenity { get; set; }
    public virtual Property Property { get; set; }
}