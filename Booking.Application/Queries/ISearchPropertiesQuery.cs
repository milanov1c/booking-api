using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface ISearchPropertiesQuery : IQuery<PropertySearchDTO, PagedResponse<PropertyListItemDTO>>
{

}
