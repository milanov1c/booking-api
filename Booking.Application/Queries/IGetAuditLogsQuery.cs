using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetAuditLogsQuery : IQuery<AuditLogSearchDTO, PagedResponse<AuditLogDTO>>
{
}
