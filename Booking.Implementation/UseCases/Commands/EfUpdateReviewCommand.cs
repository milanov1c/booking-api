using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfUpdateReviewCommand : EfUseCase, IUpdateReviewCommand
{
    private readonly UpdateReviewValidator _validator;

    public EfUpdateReviewCommand(BookingDbContext context, UpdateReviewValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(UpdateReviewDTO request)
    {
        _validator.ValidateAndThrow(request);

        var review = ctx.Reviews.First(r => r.Id == request.Id);

        review.Rating = request.Rating;
        review.Comment = request.Comment;
        review.UpdatedAt = DateTime.UtcNow;

        ctx.SaveChanges();
    }

    public string Name => "Update Review";
    public string Id => "ef-update-review";
}
