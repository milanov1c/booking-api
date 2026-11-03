using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Booking.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    //[Authorize(Roles = "Tenant, Landlord")]
    [HttpPost]
    public IActionResult Post(
        [FromBody] AddReservationDTO dto,
        [FromServices] IAddReservationCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        handler.ExecuteCommand(cmd, dto);
        return StatusCode(201);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Get(
        [FromQuery] GetUserReservationsDTO search,
        [FromServices] IGetUserReservationsQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, search);
        return Ok(result);
    }

   // [Authorize(Roles = "Landlord, Admin")]
    [HttpGet]
    [Route("landlord")]
    public IActionResult Get(
        [FromServices] IGetLandlordReservationsQuery query,
        [FromServices] UseCaseHandler handler
        )
    {
        var result = handler.ExecuteQuery(query, new EmptyQueryParams());
        return Ok(result);
    }
    //[Authorize(Roles = "Landlord, Admin")]
    [Route("confirm")]
    [HttpPost]
    public IActionResult Post(
        [FromBody] ConfirmReservationDTO dto,
        [FromServices] IConfirmReservationCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        handler.ExecuteCommand(cmd, dto);
        return NoContent();
    }


   // [Authorize(Roles = "Tenant, Landlord, Admin")]
    [Route("cancel")]
    [HttpPost]
    public IActionResult Post(
        [FromBody] CancelReservationDTO dto,
        [FromServices] ICancelReservationCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        handler.ExecuteCommand(cmd, dto);
        return NoContent();
    }
}
