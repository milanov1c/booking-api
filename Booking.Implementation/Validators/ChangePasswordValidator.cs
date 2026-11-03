using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordDTO>
{
    public ChangePasswordValidator(BookingDbContext context, IApplicationUser user)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Current password is required");

        RuleFor(x => x.OldPassword)
            .Must(oldPassword =>
            {
                var hash = context.Users.Where(u => u.Id == user.Id).Select(u => u.Password).FirstOrDefault();
                return hash != null && BCrypt.Net.BCrypt.Verify(oldPassword, hash);
            })
            .WithMessage("Current password is incorrect");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters long");

        RuleFor(x => x)
            .Must(x => x.NewPassword != x.OldPassword)
            .WithMessage("New password must be different from the current password");
    }
}
