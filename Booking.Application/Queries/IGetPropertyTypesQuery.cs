using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetPropertyTypesQuery : IQuery<EmptyQueryParams, List<PropertyTypeDTO>>
{

}
