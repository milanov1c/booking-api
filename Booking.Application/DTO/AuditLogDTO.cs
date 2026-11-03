namespace Booking.Application.DTO;

public class AuditLogDTO
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string UserEmail { get; set; }
    public string CommandId { get; set; }
    public string CommandName { get; set; }
    public DateTime ExecutedAt { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}
