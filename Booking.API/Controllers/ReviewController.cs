using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        [Authorize(Roles="Tenant")]
        [HttpPost]
        public IActionResult Post(
            [FromBody] AddReviewDTO dto,
            [FromServices] IAddReviewCommand cmd,
            [FromServices] UseCaseHandler handler)
        {
            handler.ExecuteCommand(cmd, dto);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody] UpdateReviewDTO dto,
            [FromServices] IUpdateReviewCommand cmd,
            [FromServices] UseCaseHandler handler)
        {
            dto.Id = id;
            handler.ExecuteCommand(cmd, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] IDeleteReviewCommand cmd,
            [FromServices] UseCaseHandler handler)
        {
            handler.ExecuteCommand(cmd, new DeleteReviewDTO { Id = id });
            return NoContent();
        }

        [HttpGet("property/{propertyId}")]
        public IActionResult GetByProperty(
            int propertyId,
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromServices] IGetPropertyReviewsQuery query,
            [FromServices] UseCaseHandler handler)
        {
            var param = new PropertyReviewsSearchDTO
            {
                PropertyId = propertyId,
                Page = page > 0 ? page : 1,
                PageSize = pageSize > 0 ? pageSize : 20,
            };

            var result = handler.ExecuteQuery(query, param);
            return Ok(result);
        }
    }
}
