using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetUserReservationsQuery : IQuery<GetUserReservationsDTO, PagedResponse<ReservationDTO>>
{

}
