using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class UpdatePropertyRoomValidator : AbstractValidator<UpdatePropertyRoomDTO>
{
    public UpdatePropertyRoomValidator(BookingDbContext context, IApplicationUser user)
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
            .WithMessage("You are not allowed to update this property room");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

        // Room names only need to be unique within the same property - two
        // different properties should both be able to have a "Room 1".
        RuleFor(x => x)
            .Must(x =>
            {
                var propertyId = context.PropertyRooms.Where(r => r.Id == x.Id)
                    .Select(r => (int?)r.PropertyId).FirstOrDefault();
                return !context.PropertyRooms.Any(r => r.Name == x.Name && r.Id != x.Id && r.PropertyId == propertyId);
            })
            .WithMessage("Property room with that name already exists for this property")
            .OverridePropertyName("Name");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than 0");
    }
}
