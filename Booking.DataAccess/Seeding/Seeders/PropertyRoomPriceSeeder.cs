using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

public class PropertyRoomPriceSeeder : EntitySeeder<PropertyRoomPrice>
{
    private static readonly decimal[] NightlyPrices =
        [95m, 180m, 140m, 220m, 110m, 130m, 210m, 75m, 260m, 120m];

    public override int Order => SeedOrder.PropertyRoomPrice;

    protected override DbSet<PropertyRoomPrice> GetDbSet(BookingDbContext context) => context.PropertyRoomPrices;

    protected override IEnumerable<PropertyRoomPrice> GenerateEntities(BookingDbContext context, int count)
    {
        var propertyRooms = context.PropertyRooms.OrderBy(x => x.Id).ToList();
        var priceDate = SeedDateHelper.ToUtc(DateTime.UtcNow.Date.AddDays(1));

        return propertyRooms.Select((room, index) => new PropertyRoomPrice
        {
            PropertyRoomId = room.Id,
            Price = NightlyPrices[index % NightlyPrices.Length],
            Date = priceDate.AddDays(index)
        });
    }
}
