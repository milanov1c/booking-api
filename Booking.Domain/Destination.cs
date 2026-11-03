namespace Booking.Domain;

public class Destination : BaseEntity
{
    public string City { get; set; }

    public int CountryId { get; set; }
    
    public virtual Country Country { get; set; }
    
    public virtual ICollection<Property> Properties { get; set; } = new HashSet<Property>();
}