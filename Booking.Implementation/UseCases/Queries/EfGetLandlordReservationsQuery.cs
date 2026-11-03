using Booking.Application;
using Booking.Application.DTO;
using Booking.Application.Extensions;
using Booking.Application.Queries;
using Booking.DataAccess;
using Booking.Domain;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetLandlordReservationsQuery : EfUseCase, IGetLandlordReservationsQuery
{
    private readonly IApplicationUser _user;
    public EfGetLandlordReservationsQuery(BookingDbContext context, IApplicationUser user) : base(context)
    {
        _user = user;
    }

    public List<LandlordReservationDTO> Execute(EmptyQueryParams param)
    {
        // Admins can view this page too, but only reservations for properties
        // they personally own - same scope as a Landlord.
        var reservations = ctx.Reservations
            .Where(x => x.PropertyRoom.Property.OwnerId == _user.Id)
            .Select(x=> new LandlordReservationDTO
            {
                Id = x.Id,
                Name = x.PropertyRoom.Property.Name,
                PropertyRoomName = x.PropertyRoom.Name,
                Status = x.Status.ToString(),
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Tenant = x.User.FirstName + " " + x.User.LastName,
                
            }).ToList();

        return reservations;
    }

    public string Name => "Get Landlord Reservations";
    public string Id => "ef-get-landlord-reservations";
}