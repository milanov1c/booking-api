using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

public class PropertyTypeSeeder : EntitySeeder<PropertyType>
{
    public override int Order => SeedOrder.PropertyType;

    protected override DbSet<PropertyType> GetDbSet(BookingDbContext context) => context.PropertyTypes;

    protected override IEnumerable<PropertyType> GenerateEntities(BookingDbContext context, int count) =>
        BookingSeedData.PropertyTypes.Select(name => new PropertyType { Name = name });
}
