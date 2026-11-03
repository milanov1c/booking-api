using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetCountriesQuery : IQuery<EmptyQueryParams, List<CountryDTO>>
{

}
