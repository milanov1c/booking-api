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
public class CountryController : ControllerBase

{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Post(
        [FromBody] AddCountryDTO request,
        [FromServices] IAddCountryCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
       handler.ExecuteCommand(cmd, request);
       return Ok();
    }

    [HttpGet]
    public IActionResult Get(
        [FromServices] IGetCountriesQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, new EmptyQueryParams());
        return Ok(result);
    }
}