using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetLandlordStatsQuery : IQuery<EmptyQueryParams, LandlordStatsDTO>
{
}
