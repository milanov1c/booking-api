namespace Booking.Domain;

public class Role : BaseEntity
{
    public string Name { get; set; }
    
    public virtual ICollection<RoleUseCase> UseCases { get; set; } = new HashSet<RoleUseCase>();
    public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    
}