using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;
using Booking.Domain;

namespace Booking.Implementation.UseCases.Queries;

public class EfCheckPropertyRoomAvailabilityQuery : EfUseCase, ICheckPropertyRoomAvailabilityQuery
{
    public EfCheckPropertyRoomAvailabilityQuery(BookingDbContext context) : base(context)
    {
    }

    public PropertyRoomAvailabilityDTO Execute(CheckPropertyRoomAvailabilityDTO param)
    {
        if (!ctx.PropertyRooms.Any(r => r.Id == param.PropertyRoomId))
        {
            return null;
        }

        // Normalize to Kind=Utc so the values line up with what's actually
        // stored (and Npgsql doesn't reject an Unspecified-kind DateTime
        // when this gets translated to SQL), same pattern used everywhere
        // else dates are compared against PropertyRoomPrices/Reservations.
        var startDate = DateTime.SpecifyKind(param.StartDate.Date, DateTimeKind.Utc);
        var endDate = DateTime.SpecifyKind(param.EndDate.Date, DateTimeKind.Utc);

        var isAvailable = !ctx.Reservations.Any(r =>
            r.PropertyRoomId == param.PropertyRoomId &&
            r.Status != ReservationStatus.Cancelled &&
            r.StartDate.Date < endDate &&
            r.EndDate.Date > startDate);

        var nights = (endDate - startDate).Days;

        var pricedNights = ctx.PropertyRoomPrices.Count(p =>
            p.PropertyRoomId == param.PropertyRoomId &&
            p.Date.Date >= startDate &&
            p.Date.Date < endDate);

        return new PropertyRoomAvailabilityDTO
        {
            PropertyRoomId = param.PropertyRoomId,
            StartDate = param.StartDate,
            EndDate = param.EndDate,
            IsAvailable = isAvailable,
            PriceIsSet = nights > 0 && pricedNights == nights,
        };
    }

    public string Name => "Check Property Room Availability";
    public string Id => "ef-check-property-room-availability";
}
