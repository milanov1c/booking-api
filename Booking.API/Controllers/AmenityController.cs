using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.Domain;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AmenityController : ControllerBase
{
    [HttpPost]
   // [Authorize(Roles = "Admin")]
    public IActionResult Post(
        [FromBody] AddAmenityDTO dto,
        [FromServices] IAddAmenityCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        handler.ExecuteCommand(cmd, dto);
        return StatusCode(201);
    }

    [Route("attach")]
    [HttpPost]
   // [Authorize(Roles = "Landlord, Admin")]
    public IActionResult Post(
        [FromBody] AttachAmenityDTO dto,
        [FromServices] IAttachAmenityCommand cmd,
        [FromServices] UseCaseHandler handler
        )
    {
        handler.ExecuteCommand(cmd, dto);
        return Ok();
    }

    [HttpGet]
    public IActionResult Get(
        [FromServices] IGetAmenitiesQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, new EmptyQueryParams());
        return Ok(result);
    }

    [Route("detach")]
    [HttpDelete]
   // [Authorize(Roles = "Landlord, Admin")]
    public IActionResult Detach(
        [FromQuery] int propertyId,
        [FromQuery] int amenityId,
        [FromServices] IDetachAmenityCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        handler.ExecuteCommand(cmd, new DetachAmenityDTO { PropertyId = propertyId, AmenityId = amenityId });
        return NoContent();
    }
}