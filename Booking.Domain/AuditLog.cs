namespace Booking.Domain;

public class AuditLog : BaseEntity
{
    // Intentionally not a foreign key: audit entries must survive even for
    // guest/unauthenticated attempts (no matching User row) and must remain
    // in place even if the referenced User is later deleted.
    public int? UserId { get; set; }
    public string UserEmail { get; set; }

    public string CommandId { get; set; }
    public string CommandName { get; set; }

    public DateTime ExecutedAt { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}
