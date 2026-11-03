using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class DetachUseCaseValidator : AbstractValidator<DetachUseCaseDTO>
{
    public DetachUseCaseValidator(BookingDbContext context)
    {
        RuleFor(x => x)
            .Must(x => context.RoleUseCases.Any(ru => ru.RoleId == x.RoleId && ru.UseCaseId == x.UseCaseId))
            .WithMessage("This role isn't attached to that use case");
    }
}
