using Booking.Application;
using Booking.Application.DTO;
using Booking.Application.Extensions;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetPropertyReviewsQuery : EfUseCase, IGetPropertyReviewsQuery
{
    public EfGetPropertyReviewsQuery(BookingDbContext context) : base(context)
    {
    }

    public PagedResponse<ReviewDTO> Execute(PropertyReviewsSearchDTO param)
    {
        var query = ctx.Reviews
            .Where(r => r.PropertyId == param.PropertyId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReviewDTO
            {
                Id = r.Id,
                UserId = r.UserId,
                UserFullName = r.User.FirstName + " " + r.User.LastName,
                PropertyId = r.PropertyId,
                PropertyName = r.Property.Name,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
            });

        return query.ToPagedResponse(param.Page, param.PageSize);
    }

    public string Name => "Get Property Reviews";
    public string Id => "ef-get-property-reviews";
}
