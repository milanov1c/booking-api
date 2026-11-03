using Booking.Application.DTO;

namespace Booking.Application.Queries;

public interface IGetPropertyReviewsQuery : IQuery<PropertyReviewsSearchDTO, PagedResponse<ReviewDTO>>
{

}
