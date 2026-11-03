namespace Booking.DataAccess.Seeding;

public interface IDataSeeder
{
    int Order { get; }

    void Seed(BookingDbContext context, int count, bool force = false);
}
