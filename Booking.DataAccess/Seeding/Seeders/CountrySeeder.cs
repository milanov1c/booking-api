using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

public class CountrySeeder : EntitySeeder<Country>
{
    public override int Order => SeedOrder.Country;

    protected override DbSet<Country> GetDbSet(BookingDbContext context) => context.Countries;

    protected override IEnumerable<Country> GenerateEntities(BookingDbContext context, int count) =>
        BookingSeedData.EuropeanDestinations
            .Select(x => x.Country)
            .Distinct()
            .Select(name => new Country { Name = name });
}
