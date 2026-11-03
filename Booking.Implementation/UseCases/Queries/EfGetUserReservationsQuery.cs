using Booking.Application;
using Booking.Application.DTO;
using Booking.Application.Extensions;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetUserReservationsQuery : EfUseCase, IGetUserReservationsQuery
{
    private readonly IApplicationUser _user;

    public EfGetUserReservationsQuery(BookingDbContext context, IApplicationUser user) : base(context)
    {
        _user = user;
    }

    public PagedResponse<ReservationDTO> Execute(GetUserReservationsDTO param)
    {
        var query = ctx.Reservations
            .Where(r => r.UserId == _user.Id)
            .OrderByDescending(r => r.StartDate)
            .Select(r => new ReservationDTO
            {
                Id = r.Id,
                PropertyId = r.PropertyRoom.PropertyId,
                PropertyName = r.PropertyRoom.Property.Name,
                PropertyRoomId = r.PropertyRoomId,
                PropertyRoomName = r.PropertyRoom.Name,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                TotalPrice = r.TotalPrice,
                Status = r.Status.ToString(),
            });

        return query.ToPagedResponse(param.Page, param.PageSize);
    }

    public string Name => "Get User Reservations";
    public string Id => "ef-get-user-reservations";
}
