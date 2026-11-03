using Booking.Application;
using Booking.Application.DTO;
using Booking.Application.Extensions;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetPropertyRoomPricesQuery : EfUseCase, IGetPropertyRoomPricesQuery
{
    public EfGetPropertyRoomPricesQuery(BookingDbContext context) : base(context)
    {
    }

    public PagedResponse<PropertyRoomPriceDTO> Execute(PropertyRoomPriceSearchDTO param)
    {
        var projected = ctx.PropertyRoomPrices
            .Where(p => p.PropertyRoomId == param.PropertyRoomId)
            .OrderBy(p => p.Date)
            .Select(p => new PropertyRoomPriceDTO
            {
                Id = p.Id,
                Date = p.Date,
                Price = p.Price,
            });

        return projected.ToPagedResponse(param.Page, param.PageSize);
    }

    public string Name => "Get Property Room Prices";
    public string Id => "ef-get-property-room-prices";
}
