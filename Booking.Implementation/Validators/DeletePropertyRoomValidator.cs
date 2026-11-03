using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class DeletePropertyRoomValidator : AbstractValidator<DeletePropertyRoomDTO>
{
    public DeletePropertyRoomValidator(BookingDbContext context, IApplicationUser user)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .Must(id => context.PropertyRooms.Any(r => r.Id == id))
            .WithMessage("Property room doesn't exist")
            .Must(id =>
            {
                var isAdmin = string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase);
                if (isAdmin)
                {
                    return true;
                }

                var ownerId = context.PropertyRooms.Where(r => r.Id == id)
                    .Select(r => (int?)r.Property.OwnerId).FirstOrDefault();
                return ownerId == user.Id;
            })
            .WithMessage("You are not allowed to delete this property room")
            .Must(id => !context.Reservations.Any(r => r.PropertyRoomId == id))
            .WithMessage("Property room cannot be deleted because it has reservations (past or upcoming)");
    }
}
