namespace Booking.Domain;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; } = true;

    public int RoleId { get; set; }
    public virtual Role Role { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    public virtual ICollection<AuthToken> AuthTokens { get; set; } = new HashSet<AuthToken>();
    public virtual ICollection<Property> OwnedProperties { get; set; } = new HashSet<Property>();
    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
}