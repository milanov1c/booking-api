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
public class DestinationController : ControllerBase

{
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Post(
        [FromBody] AddDestinationDTO request,
        [FromServices] IAddDestinationCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        handler.ExecuteCommand(cmd, request);
        return Ok();
    }

    [HttpGet]
    public IActionResult Get(
        [FromServices] IGetDestinationsQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, new EmptyQueryParams());
        return Ok(result);
    }
}