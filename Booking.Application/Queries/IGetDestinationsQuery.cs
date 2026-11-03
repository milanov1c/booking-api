using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetDestinationsQuery : IQuery<EmptyQueryParams, List<DestinationDTO>>
{

}
