using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

/// <summary>
/// Adds a few differently-priced date entries for every room created by
/// ExtraPropertyRoomSeeder (rooms whose name ends with " room 2"), so search-by-price
/// has more variety to filter on. Runs right after PropertyRoomPriceSeeder.
///
/// Implemented as a plain IDataSeeder for the same reason as ExtraPropertyRoomSeeder:
/// the PropertyRoomPrices table is no longer empty by the time this runs, so
/// idempotency is checked per (PropertyRoomId, Date) pair instead of via Any().
/// </summary>
public class ExtraPropertyRoomPriceSeeder : IDataSeeder
{
    private const string ExtraRoomMarker = " room 2";

    // Distinct price points per room, spread across consecutive nights,
    // deliberately different from PropertyRoomPriceSeeder.NightlyPrices.
    private static readonly decimal[][] PriceSets =
    [
        [70m, 85m, 65m],
        [150m, 175m, 190m],
        [305m, 280m, 260m],
        [45m, 55m, 40m],
        [199m, 210m, 225m],
        [88m, 92m, 79m],
        [340m, 360m, 330m],
        [60m, 58m, 63m],
        [120m, 135m, 128m]
    ];

    public int Order => SeedOrder.PropertyRoomPrice + 1;

    public void Seed(BookingDbContext context, int count, bool force = false)
    {
        var extraRooms = context.PropertyRooms
            .Where(r => r.Name.EndsWith(ExtraRoomMarker))
            .OrderBy(r => r.Id)
            .ToList();

        if (extraRooms.Count == 0)
        {
            return;
        }

        var existingKeys = context.PropertyRoomPrices
            .Select(p => new { p.PropertyRoomId, p.Date })
            .ToHashSet();

        var baseDate = SeedDateHelper.ToUtc(DateTime.UtcNow.Date.AddDays(1));
        var newPrices = new List<PropertyRoomPrice>();

        for (int i = 0; i < extraRooms.Count; i++)
        {
            var room = extraRooms[i];
            var prices = PriceSets[i % PriceSets.Length];

            for (int day = 0; day < prices.Length; day++)
            {
                var date = baseDate.AddDays(day);

                if (!force && existingKeys.Contains(new { PropertyRoomId = room.Id, Date = date }))
                {
                    continue;
                }

                newPrices.Add(new PropertyRoomPrice
                {
                    PropertyRoomId = room.Id,
                    Price = prices[day],
                    Date = date
                });
            }
        }

        if (newPrices.Count == 0)
        {
            return;
        }

        context.PropertyRoomPrices.AddRange(newPrices);
        context.SaveChanges();
    }
}
