using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

public class RoleSeeder : EntitySeeder<Role>
{
    public override int Order => SeedOrder.Role;

    protected override DbSet<Role> GetDbSet(BookingDbContext context) => context.Roles;

    protected override IEnumerable<Role> GenerateEntities(BookingDbContext context, int count) =>
        BookingSeedData.Roles.Select(name => new Role { Name = name });
}
