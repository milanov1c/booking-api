namespace Booking.Domain;

public class Review : BaseEntity
{
    public int UserId { get; set; }
    
    public virtual User User { get; set; }
    public int PropertyId { get; set; }
    
    public virtual Property Property { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}