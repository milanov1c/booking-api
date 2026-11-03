namespace Booking.Domain;

public class Country : BaseEntity
{
    public string Name { get; set; }
    
    public virtual ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();
}