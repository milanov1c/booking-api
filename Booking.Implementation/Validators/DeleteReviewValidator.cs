using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class DeleteReviewValidator : AbstractValidator<DeleteReviewDTO>
{
    public DeleteReviewValidator(BookingDbContext context, IApplicationUser user)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .Must(id => context.Reviews.Any(r => r.Id == id))
            .WithMessage("Review doesn't exist")
            .Must(id =>
            {
                var isAdmin = string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase);
                if (isAdmin)
                {
                    return true;
                }

                var authorId = context.Reviews.Where(r => r.Id == id).Select(r => (int?)r.UserId).FirstOrDefault();
                return authorId == user.Id;
            })
            .WithMessage("You are not allowed to delete this review");
    }
}
