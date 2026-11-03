namespace Booking.Domain;

public class UseCase : BaseEntity
{
    public string UseCaseId { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<RoleUseCase> UseCases { get; set; } = new HashSet<RoleUseCase>();
}