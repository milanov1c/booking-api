using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

public class UserSeeder : EntitySeeder<User>
{
    public override int Order => SeedOrder.User;

    protected override DbSet<User> GetDbSet(BookingDbContext context) => context.Users;

    protected override IEnumerable<User> GenerateEntities(BookingDbContext context, int count)
    {
        var roles = context.Roles.ToDictionary(x => x.Name, x => x.Id);
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(BookingSeedData.DefaultPassword);

        return BookingSeedData.Users.Select(x => new User
        {
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Password = passwordHash,
            RoleId = roles[x.RoleName]
        });
    }
}
