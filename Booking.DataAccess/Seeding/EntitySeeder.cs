using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding;

public abstract class EntitySeeder<T> : IDataSeeder where T : BaseEntity
{
    public abstract int Order { get; }

    protected abstract DbSet<T> GetDbSet(BookingDbContext context);

    protected abstract IEnumerable<T> GenerateEntities(BookingDbContext context, int count);

    public void Seed(BookingDbContext context, int count, bool force = false)
    {
        if (!force && GetDbSet(context).Any())
        {
            return;
        }

        GetDbSet(context).AddRange(GenerateEntities(context, count));
        context.SaveChanges();
    }
}
