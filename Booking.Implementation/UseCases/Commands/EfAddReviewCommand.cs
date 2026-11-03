using Booking.Application;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfAddReviewCommand : EfUseCase, IAddReviewCommand
{
    private AddReviewValidator _validator;
    private readonly IApplicationUser _user;
    public EfAddReviewCommand(BookingDbContext context, AddReviewValidator validator, IApplicationUser user) : base(context)
    {
        _validator = validator;
        _user = user;
    }
    

    public void Execute(AddReviewDTO request)
    {
        _validator.ValidateAndThrow(request);
        Review review = new Review();
        review.PropertyId = request.PropertyId;
        review.Rating = request.Rating;
        review.Comment = request.Comment;
        review.UserId=_user.Id;
        ctx.Add(review);
        ctx.SaveChanges();
    }

    public string Name => "Add review";
    public string Id => "ef-add-review";
}