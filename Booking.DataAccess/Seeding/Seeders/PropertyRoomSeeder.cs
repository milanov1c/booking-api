using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

public class PropertyRoomSeeder : EntitySeeder<PropertyRoom>
{
    private static readonly int[] RoomCapacities = [2, 2, 3, 4, 2, 2, 3, 4, 6, 2];

    public override int Order => SeedOrder.PropertyRoom;

    protected override DbSet<PropertyRoom> GetDbSet(BookingDbContext context) => context.PropertyRooms;

    protected override IEnumerable<PropertyRoom> GenerateEntities(BookingDbContext context, int count)
    {
        var properties = context.Properties.OrderBy(x => x.Id).ToList();

        return properties.Select((property, index) => new PropertyRoom
        {
            Name = Truncate($"{property.Name} room 1", 50),
            Capacity = RoomCapacities[index % RoomCapacities.Length],
            PropertyId = property.Id
        });
    }

    private static string Truncate(string value, int maxLength) =>
        value.Length <= maxLength ? value : value[..maxLength];
}
