using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetRolesQuery : IQuery<EmptyQueryParams, List<RoleListItemDTO>>
{

}
