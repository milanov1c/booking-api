using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

public class DestinationSeeder : EntitySeeder<Destination>
{
    public override int Order => SeedOrder.Destination;

    protected override DbSet<Destination> GetDbSet(BookingDbContext context) => context.Destinations;

    protected override IEnumerable<Destination> GenerateEntities(BookingDbContext context, int count)
    {
        var countries = context.Countries.ToDictionary(x => x.Name, x => x.Id);

        return BookingSeedData.EuropeanDestinations.Select(x => new Destination
        {
            City = x.City,
            CountryId = countries[x.Country]
        });
    }
}
