using Booking.Application;
using Booking.Application.DTO;
using Booking.Application.Extensions;
using Booking.Application.Queries;
using Booking.DataAccess;
using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetAuditLogsQuery : EfUseCase, IGetAuditLogsQuery
{
    public EfGetAuditLogsQuery(BookingDbContext context) : base(context)
    {
    }

    public PagedResponse<AuditLogDTO> Execute(AuditLogSearchDTO param)
    {
        IQueryable<AuditLog> query = ctx.AuditLogs;

        if (!string.IsNullOrWhiteSpace(param.UserEmail))
        {
            query = query.Where(a => EF.Functions.ILike(a.UserEmail, $"%{param.UserEmail}%"));
        }

        if (!string.IsNullOrWhiteSpace(param.CommandName))
        {
            query = query.Where(a => EF.Functions.ILike(a.CommandName, $"%{param.CommandName}%"));
        }

        if (param.Success.HasValue)
        {
            query = query.Where(a => a.Success == param.Success.Value);
        }

        if (param.From.HasValue)
        {
            var from = DateTime.SpecifyKind(param.From.Value, DateTimeKind.Utc);
            query = query.Where(a => a.ExecutedAt >= from);
        }

        if (param.To.HasValue)
        {
            var to = DateTime.SpecifyKind(param.To.Value, DateTimeKind.Utc);
            query = query.Where(a => a.ExecutedAt <= to);
        }

        var projected = query
            .OrderByDescending(a => a.ExecutedAt)
            .Select(a => new AuditLogDTO
            {
                Id = a.Id,
                UserId = a.UserId,
                UserEmail = a.UserEmail,
                CommandId = a.CommandId,
                CommandName = a.CommandName,
                ExecutedAt = a.ExecutedAt,
                Success = a.Success,
                ErrorMessage = a.ErrorMessage,
            });

        return projected.ToPagedResponse(param.Page, param.PageSize);
    }

    public string Name => "Get Audit Logs";
    public string Id => "ef-get-audit-logs";
}
