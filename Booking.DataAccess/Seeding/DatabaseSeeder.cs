namespace Booking.DataAccess.Seeding;

public class DatabaseSeeder
{
    private readonly IEnumerable<IDataSeeder> _seeders;

    public DatabaseSeeder(IEnumerable<IDataSeeder> seeders)
    {
        _seeders = seeders;
    }

    public void Seed(BookingDbContext context, int count = 10, bool force = false)
    {
        foreach (var seeder in _seeders.OrderBy(s => s.Order))
        {
            seeder.Seed(context, count, force);
        }
    }

    public void Reseed(BookingDbContext context, int count = 10)
    {
        DatabaseCleaner.ClearAll(context);
        Seed(context, count, force: true);
    }
}
