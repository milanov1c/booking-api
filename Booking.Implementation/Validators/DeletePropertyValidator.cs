using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class DeletePropertyValidator : AbstractValidator<DeletePropertyDTO>
{
    public DeletePropertyValidator(BookingDbContext context, IApplicationUser user)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .Must(id => context.Properties.Any(p => p.Id == id))
            .WithMessage("Property doesn't exist")
            .Must(id =>
            {
                var isAdmin = string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase);
                if (isAdmin)
                {
                    return true;
                }

                var ownerId = context.Properties.Where(p => p.Id == id).Select(p => (int?)p.OwnerId).FirstOrDefault();
                return ownerId == user.Id;
            })
            .WithMessage("You are not allowed to delete this property")
            .Must(id => !context.Reservations.Any(r => r.PropertyRoom.PropertyId == id))
            .WithMessage("Property cannot be deleted because it has reservations (past or upcoming)");
    }
}
