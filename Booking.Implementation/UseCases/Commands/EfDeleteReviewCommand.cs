using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfDeleteReviewCommand : EfUseCase, IDeleteReviewCommand
{
    private readonly DeleteReviewValidator _validator;

    public EfDeleteReviewCommand(BookingDbContext context, DeleteReviewValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(DeleteReviewDTO request)
    {
        _validator.ValidateAndThrow(request);

        var review = ctx.Reviews.First(r => r.Id == request.Id);

        ctx.Reviews.Remove(review);
        ctx.SaveChanges();
    }

    public string Name => "Delete Review";
    public string Id => "ef-delete-review";
}
