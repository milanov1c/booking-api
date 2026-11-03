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
public class PropertyController : ControllerBase
{
    [Authorize(Roles = "Admin, Landlord")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public IActionResult Post(
        [FromForm] AddPropertyRequest request,
        [FromServices] IAddPropertyCommand cmd,
        [FromServices] UseCaseHandler handler,
        [FromServices] IApplicationUser currentUser)
    {
       
        var isLandlord = string.Equals(currentUser.Role, "Landlord", StringComparison.OrdinalIgnoreCase);
        var landlordId = isLandlord ? currentUser.Id : request.LandlordId;

        var dto = new AddPropertyDTO
        {
            Name = request.Name,
            Description = request.Description,
            Address = request.Address,
            PropertyTypeId = request.PropertyTypeId,
            DestinationId = request.DestinationId,
            LandlordId = landlordId,
            Image = request.Image != null ? new FormFileUploadedFile(request.Image) : null,
        };

        handler.ExecuteCommand(cmd, dto);
        return StatusCode(201);
    }

    [Authorize(Roles = "Admin, Landlord")]
    [HttpPost("attach")]
    [Consumes("multipart/form-data")]
    public IActionResult Attach(
        [FromForm] AttachPropertyImagesRequest request,
        [FromServices] IAttachPropertyImagesCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        var dto = new AttachPropertyImagesDTO
        {
            PropertyId = request.PropertyId,
            Images = request.Images
                .Select(image => (IUploadedFile)new FormFileUploadedFile(image))
                .ToList(),
        };

        handler.ExecuteCommand(cmd, dto);
        return StatusCode(201);
    }

    [HttpGet]
    public IActionResult Get(
        [FromQuery] PropertySearchDTO search,
        [FromServices] ISearchPropertiesQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, search);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(
        int id,
        [FromServices] IGetPropertyQuery query,
        [FromServices] UseCaseHandler handler)
    {
        var result = handler.ExecuteQuery(query, id);

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
        [FromBody] UpdatePropertyDTO dto,
        [FromServices] IUpdatePropertyCommand cmd,
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
        [FromServices] IDeletePropertyCommand cmd,
        [FromServices] UseCaseHandler handler)
    {
        handler.ExecuteCommand(cmd, new DeletePropertyDTO { Id = id });
        return NoContent();
    }
}
