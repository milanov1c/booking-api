using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetAmenitiesQuery : IQuery<EmptyQueryParams, List<AmenityDTO>>
{

}
