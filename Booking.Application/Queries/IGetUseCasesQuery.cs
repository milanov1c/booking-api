using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetUseCasesQuery : IQuery<EmptyQueryParams, List<UseCaseListItemDTO>>
{

}
