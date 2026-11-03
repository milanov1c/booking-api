using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class UpdateUserStatusValidator : AbstractValidator<UpdateUserStatusDTO>
{
    public UpdateUserStatusValidator(BookingDbContext context)
    {
        RuleFor(x => x.UserId)
            .Must(id => context.Users.Any(u => u.Id == id))
            .WithMessage("User doesn't exist");
    }
}
