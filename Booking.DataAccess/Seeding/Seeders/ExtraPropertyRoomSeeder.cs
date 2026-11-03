using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

/// <summary>
/// Adds a second room to every already-seeded property, except properties whose
/// PropertyType is "house". Runs right after PropertyRoomSeeder (same Order + 1).
///
/// Implemented as a plain IDataSeeder (not EntitySeeder&lt;T&gt;) because the
/// PropertyRooms table is no longer empty by the time this runs (PropertyRoomSeeder
/// already added one room per property), so the base "skip if Any()" guard would
/// always skip it. Instead, idempotency is checked per-room by name.
/// </summary>
public class ExtraPropertyRoomSeeder : IDataSeeder
{
    private const string ExcludedPropertyType = "house";
    private static readonly int[] ExtraRoomCapacities = [2, 3, 5, 2, 4, 6, 3, 2, 5];

    public int Order => SeedOrder.PropertyRoom + 1;

    public void Seed(BookingDbContext context, int count, bool force = false)
    {
        var properties = context.Properties
            .Include(p => p.PropertyType)
            .Where(p => p.PropertyType.Name != ExcludedPropertyType)
            .OrderBy(p => p.Id)
            .ToList();

        var existingNames = context.PropertyRooms
            .Select(r => r.Name)
            .ToHashSet();

        var newRooms = new List<PropertyRoom>();

        for (int i = 0; i < properties.Count; i++)
        {
            var property = properties[i];
            var name = Truncate($"{property.Name} room 2", 50);

            if (!force && existingNames.Contains(name))
            {
                continue;
            }

            newRooms.Add(new PropertyRoom
            {
                Name = name,
                Capacity = ExtraRoomCapacities[i % ExtraRoomCapacities.Length],
                PropertyId = property.Id
            });
        }

        if (newRooms.Count == 0)
        {
            return;
        }

        context.PropertyRooms.AddRange(newRooms);
        context.SaveChanges();
    }

    private static string Truncate(string value, int maxLength) =>
        value.Length <= maxLength ? value : value[..maxLength];
}
