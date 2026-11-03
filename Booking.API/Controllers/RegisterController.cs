using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    [HttpPost]
    public IActionResult Post(
        [FromServices] IRegisterUserCommand cmd,
        [FromServices] UseCaseHandler handler,
        [FromBody] RegisterUserDTO dto)
    {
        handler.ExecuteCommand(cmd, dto);
        return StatusCode(201);
    }

    // Anonymous, deliberately restricted list of self-registerable roles.
    // GET /api/Role is Admin-only and can't be called from the public
    // registration form, and exposing the real role IDs (which depend on
    // seed order) any other way led to the registration form hardcoding
    // guessed IDs that didn't match the actual database - causing a
    // foreign key violation on Users.RoleId. This returns the real IDs,
    // excluding "Admin" so nobody can self-register as an administrator.
    [HttpGet("roles")]
    public IActionResult GetRegistrableRoles(
        [FromServices] IGetRolesQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var roles = handler.ExecuteQuery(query, new EmptyQueryParams());
        var registrable = roles.Where(r => r.Name != "Admin").ToList();
        return Ok(registrable);
    }
}