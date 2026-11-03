using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetPropertyRoomPricesQuery : IQuery<PropertyRoomPriceSearchDTO, PagedResponse<PropertyRoomPriceDTO>>
{
}
