using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

public class ReservationSeeder : EntitySeeder<Reservation>
{
    private static readonly (int TenantIndex, int RoomIndex, int StartInDays, int Nights, decimal Price)[] Reservations =
    [
        (0, 0, 7, 3, 285m),
        (1, 2, 10, 5, 700m),
        (2, 4, 14, 2, 220m),
        (3, 6, 20, 4, 840m),
        (4, 8, 25, 6, 1560m),
        (5, 1, 12, 3, 540m),
        (6, 3, 18, 4, 880m),
        (0, 5, 30, 2, 260m),
        (1, 7, 35, 3, 225m),
        (2, 9, 40, 5, 600m)
    ];

    public override int Order => SeedOrder.Reservation;

    protected override DbSet<Reservation> GetDbSet(BookingDbContext context) => context.Reservations;

    protected override IEnumerable<Reservation> GenerateEntities(BookingDbContext context, int count)
    {
        var tenantRoleId = context.Roles.First(x => x.Name == "Tenant").Id;
        var tenants = context.Users
            .Where(x => x.RoleId == tenantRoleId)
            .OrderBy(x => x.Id)
            .ToList();

        var propertyRooms = context.PropertyRooms.OrderBy(x => x.Id).ToList();

        return Reservations.Select(x =>
        {
            var startDate = SeedDateHelper.ToUtc(DateTime.UtcNow.Date.AddDays(x.StartInDays));

            return new Reservation
            {
                UserId = tenants[x.TenantIndex].Id,
                PropertyRoomId = propertyRooms[x.RoomIndex].Id,
                StartDate = startDate,
                EndDate = startDate.AddDays(x.Nights),
                TotalPrice = x.Price,
                Status = ReservationStatus.Confirmed
            };
        });
    }
}
