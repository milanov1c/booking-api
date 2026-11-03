using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetUsersQuery : IQuery<GetUsersSearchDTO, PagedResponse<UserListItemDTO>>
{
}
