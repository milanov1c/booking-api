using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.Domain;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;
[ApiController]
[Route("api/price")]
public class PropertyRoomPriceController : ControllerBase
{
  [HttpGet]
  [Authorize(Roles = "Admin, Landlord")]
  public IActionResult Get(
    [FromQuery] PropertyRoomPriceSearchDTO search,
    [FromServices] IGetPropertyRoomPricesQuery query,
    [FromServices] UseCaseHandler handler)
  {
    var result = handler.ExecuteQuery(query, search);
    return Ok(result);
  }

  [HttpPost]
  [Authorize(Roles = "Admin, Landlord")]
  public IActionResult Post(
    [FromServices] IAttachPriceCommand cmd,
    [FromServices] UseCaseHandler handler,
    [FromBody] AttachPriceDTO dto)
  {
    handler.ExecuteCommand(cmd,dto);
    return Ok();
  }

  [HttpPut("{id}")]
  [Authorize(Roles = "Admin, Landlord")]
  public IActionResult Put(
    int id,
    [FromServices] IUpdatePropertyRoomPriceCommand cmd,
    [FromServices] UseCaseHandler handler,
    [FromBody] UpdatePropertyRoomPriceDTO dto)
  {
    dto.Id = id;
    handler.ExecuteCommand(cmd, dto);
    return NoContent();
  }

  [HttpDelete("{id}")]
  [Authorize(Roles = "Admin, Landlord")]
  public IActionResult Delete(
    int id,
    [FromServices] IDeletePropertyRoomPriceCommand cmd,
    [FromServices] UseCaseHandler handler)
  {
    handler.ExecuteCommand(cmd, new DeletePropertyRoomPriceDTO { Id = id });
    return NoContent();
  }
}