using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyTypeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get(
        [FromServices] IGetPropertyTypesQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, new EmptyQueryParams());
        return Ok(result);
    }
}
