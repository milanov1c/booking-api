namespace Booking.Application.DTO;

public class AuditLogSearchDTO
{
    public string UserEmail { get; set; }
    public string CommandName { get; set; }
    public bool? Success { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
