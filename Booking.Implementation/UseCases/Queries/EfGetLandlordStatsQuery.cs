using Booking.Application;
using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;
using Booking.Domain;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetLandlordStatsQuery : EfUseCase, IGetLandlordStatsQuery
{
    private readonly IApplicationUser _user;

    public EfGetLandlordStatsQuery(BookingDbContext context, IApplicationUser user) : base(context)
    {
        _user = user;
    }

    public LandlordStatsDTO Execute(EmptyQueryParams param)
    {
        // Admins can view this page too, but only stats for properties they
        // personally own - same scope as a Landlord.
        var properties = ctx.Properties.Where(p => p.OwnerId == _user.Id);

        var reservations = ctx.Reservations
            .Where(r => r.PropertyRoom.Property.OwnerId == _user.Id);

        return new LandlordStatsDTO
        {
            TotalProperties = properties.Count(),
            TotalPropertyRooms = properties.SelectMany(p => p.PropertyRooms).Count(),
            TotalReservations = reservations.Count(),
            ConfirmedReservations = reservations.Count(r => r.Status == ReservationStatus.Confirmed),
            PendingReservations = reservations.Count(r => r.Status == ReservationStatus.Pending),
            CancelledReservations = reservations.Count(r => r.Status == ReservationStatus.Cancelled),
            TotalRevenue = reservations
                .Where(r => r.Status == ReservationStatus.Confirmed)
                .Select(r => (decimal?)r.TotalPrice).Sum() ?? 0,
            AverageRating = properties.SelectMany(p => p.Reviews)
                .Select(r => (decimal?)r.Rating).Average(),
        };
    }

    public string Name => "Get Landlord Stats";
    public string Id => "ef-get-landlord-stats";
}
