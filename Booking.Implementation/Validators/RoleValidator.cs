using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class RoleValidator : AbstractValidator<RoleDTO>
{
    public RoleValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).Must(x => !context.Roles.Any(r => r.Name == x))
            .WithMessage("Role with that name already exists");
    }
}