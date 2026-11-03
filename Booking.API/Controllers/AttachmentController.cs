using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttachmentController : ControllerBase
{
   // [Authorize(Roles = "Admin, Landlord")]
    [HttpDelete("{id}")]
    public IActionResult Delete(
        int id,
        [FromServices] IDeleteAttachmentCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        handler.ExecuteCommand(cmd, new DeleteAttachmentDTO { Id = id });
        return NoContent();
    }
}
