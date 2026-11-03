using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetLandlordReservationsQuery : IQuery<EmptyQueryParams, List<LandlordReservationDTO>>
{
    
}