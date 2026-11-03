using System.Security.Cryptography;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UseCaseController : ControllerBase
{
    private BookingDbContext _ctx;
    private UseCaseHandler _handler;

    public UseCaseController(BookingDbContext ctx, UseCaseHandler handler)
    {
        _ctx = ctx;
        _handler = handler;
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Post([FromServices] IAddUseCaseCommand cmd, [FromBody] AddUseCaseDTO dto)
    {
        _handler.ExecuteCommand(cmd, dto);
        return Ok();
    }

    [Route("attach")]
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Post([FromServices] IAttachUseCaseCommand cmd, [FromBody] AttachUseCaseDTO dto)
    {
        _handler.ExecuteCommand(cmd, dto);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Get([FromServices] IGetUseCasesQuery query)
    {
        var result = _handler.ExecuteQuery(query, new EmptyQueryParams());
        return Ok(result);
    }

    [Route("detach")]
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public IActionResult Detach(
        [FromQuery] int roleId,
        [FromQuery] int useCaseId,
        [FromServices] IDetachUseCaseCommand cmd)
    {
        _handler.ExecuteCommand(cmd, new DetachUseCaseDTO { RoleId = roleId, UseCaseId = useCaseId });
        return NoContent();
    }
}