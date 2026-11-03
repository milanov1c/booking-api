using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.Domain;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
  [Authorize(Roles = "Admin")]
  [HttpPost]
  public IActionResult Post(
    [FromServices] IAddRoleCommand cmd,
    [FromServices] UseCaseHandler handler,
    [FromBody] RoleDTO dto)
  {
    handler.ExecuteCommand(cmd, dto);
    return StatusCode(201);
  }

  [Authorize(Roles = "Admin")]
  [HttpGet]
  public IActionResult Get(
    [FromServices] IGetRolesQuery query,
    [FromServices] UseCaseHandler handler)
  {
    var result = handler.ExecuteQuery(query, new EmptyQueryParams());
    return Ok(result);
  }
}