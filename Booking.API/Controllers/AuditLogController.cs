using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Admin")]
public class AuditLogController : ControllerBase
{
    [HttpGet]
    public IActionResult Get(
        [FromQuery] AuditLogSearchDTO search,
        [FromServices] IGetAuditLogsQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, search);
        return Ok(result);
    }
}
