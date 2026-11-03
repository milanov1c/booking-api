using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleDTO>
{
    public UpdateUserRoleValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserId)
            .Must(id => context.Users.Any(u => u.Id == id))
            .WithMessage("User doesn't exist");

        RuleFor(x => x.RoleId)
            .Must(id => context.Roles.Any(r => r.Id == id))
            .WithMessage("Role doesn't exist");
    }
}
