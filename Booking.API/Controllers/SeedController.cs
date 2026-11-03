using Booking.DataAccess;
using Booking.DataAccess.Seeding.Seeders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

/// <summary>
/// On-demand data-seeding endpoints for maintenance/testing. Each action here
/// calls a single, narrowly-scoped seeder directly (not the full DatabaseSeeder
/// pipeline), so it only ever touches the rows it's responsible for and never
/// clears or re-seeds anything else.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly BookingDbContext _context;

    public SeedController(BookingDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a second room (with a few differently-priced nights) to every
    /// already-existing property except ones with PropertyType "house".
    /// Safe to call multiple times - it only adds rooms/prices that don't
    /// already exist, it never deletes or modifies anything.
    /// </summary>
    //[Authorize(Roles = "Admin")]
    [HttpPost("extra-property-rooms")]
    public IActionResult SeedExtraPropertyRooms()
    {
        new ExtraPropertyRoomSeeder().Seed(_context, 10);
        new ExtraPropertyRoomPriceSeeder().Seed(_context, 10);

        return Ok(new
        {
            Message = "Extra property rooms and prices seeded.",
            PropertyRooms = _context.PropertyRooms.Count(),
            PropertyRoomPrices = _context.PropertyRoomPrices.Count()
        });
    }
}
