using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile(
        [FromServices] IGetUserProfileQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, new GetUserProfileDTO());

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Get(
        [FromQuery] GetUsersSearchDTO search,
        [FromServices] IGetUsersQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, search);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/role")]
    public IActionResult PutRole(
        int id,
        [FromBody] UpdateUserRoleDTO dto,
        [FromServices] IUpdateUserRoleCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        dto.UserId = id;
        handler.ExecuteCommand(cmd, dto);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/status")]
    public IActionResult PutStatus(
        int id,
        [FromBody] UpdateUserStatusDTO dto,
        [FromServices] IUpdateUserStatusCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        dto.UserId = id;
        handler.ExecuteCommand(cmd, dto);
        return NoContent();
    }

    [Authorize(Roles = "Landlord, Admin")]
    [HttpGet("landlord/stats")]
    public IActionResult GetLandlordStats(
        [FromServices] IGetLandlordStatsQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, new EmptyQueryParams());
        return Ok(result);
    }

    [Authorize]
    [HttpPut("password")]
    public IActionResult ChangePassword(
        [FromBody] ChangePasswordDTO dto,
        [FromServices] IChangePasswordCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        handler.ExecuteCommand(cmd, dto);
        return NoContent();
    }
}
