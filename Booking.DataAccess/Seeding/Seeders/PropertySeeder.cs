using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

public class PropertySeeder : EntitySeeder<Property>
{
    public override int Order => SeedOrder.Property;

    protected override DbSet<Property> GetDbSet(BookingDbContext context) => context.Properties;

    protected override IEnumerable<Property> GenerateEntities(BookingDbContext context, int count)
    {
        var destinations = context.Destinations.ToDictionary(x => x.City, x => x.Id);
        var propertyTypes = context.PropertyTypes.ToDictionary(x => x.Name, x => x.Id);

        var landlords = context.Users
            .Where(u => u.Role.Name == "Landlord")
            .OrderBy(u => u.Id)
            .ToList();

        return BookingSeedData.Properties.Select((x, index) => new Property
        {
            Name = x.Name,
            Description = x.Description,
            Address = x.Address,
            DestinationId = destinations[x.City],
            PropertyTypeId = propertyTypes[x.PropertyType],
            OwnerId = landlords[index % landlords.Count].Id
        });
    }
}
