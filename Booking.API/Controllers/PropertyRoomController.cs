using Booking.API.DTO;
using Booking.API.Uploads;
using Booking.Application;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyRoomController : ControllerBase
{
    [Authorize(Roles = "Admin, Landlord")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public IActionResult Post(
        [FromForm] AddPropertyRoomRequest request,
        [FromServices] IAddPropertyRoomCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        var dto = new AddPropertyRoomDTO
        {
            Name = request.Name,
            Capacity = request.Capacity,
            PropertyId = request.PropertyId,
            Image = request.Image != null ? new FormFileUploadedFile(request.Image) : null,
        };

        handler.ExecuteCommand(cmd, dto);
        return StatusCode(201);
    }

    [Authorize(Roles = "Admin, Landlord")]
    [HttpPost("attach")]
    [Consumes("multipart/form-data")]
    public IActionResult Attach(
        [FromForm] AttachPropertyRoomImagesRequest request,
        [FromServices] IAttachPropertyRoomImagesCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        var dto = new AttachPropertyRoomImagesDTO
        {
            PropertyRoomId = request.PropertyRoomId,
            Images = request.Images
                .Select(image => (IUploadedFile)new FormFileUploadedFile(image))
                .ToList(),
        };

        handler.ExecuteCommand(cmd, dto);
        return StatusCode(201);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(
        int id,
        [FromServices] IGetPropertyRoomQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{id}/availability")]
    public IActionResult CheckAvailability(
        int id,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromServices] ICheckPropertyRoomAvailabilityQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var param = new CheckPropertyRoomAvailabilityDTO
        {
            PropertyRoomId = id,
            StartDate = startDate,
            EndDate = endDate,
        };

        var result = handler.ExecuteQuery(query, param);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [Authorize(Roles = "Admin, Landlord")]
    [HttpPut("{id}")]
    public IActionResult Put(
        int id,
        [FromBody] UpdatePropertyRoomDTO dto,
        [FromServices] IUpdatePropertyRoomCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        dto.Id = id;
        handler.ExecuteCommand(cmd, dto);
        return NoContent();
    }

    [Authorize(Roles = "Admin, Landlord")]
    [HttpDelete("{id}")]
    public IActionResult Delete(
        int id,
        [FromServices] IDeletePropertyRoomCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        handler.ExecuteCommand(cmd, new DeletePropertyRoomDTO { Id = id });
        return NoContent();
    }
}
