using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class DetachAmenityValidator : AbstractValidator<DetachAmenityDTO>
{
    public DetachAmenityValidator(BookingDbContext context, IApplicationUser user)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .Must(x => context.AmenityProperties.Any(a => a.AmenityId == x.AmenityId && a.PropertyId == x.PropertyId))
            .WithMessage("Amenity isn't attached to this property")
            .Must(x =>
            {
                var isAdmin = string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase);
                if (isAdmin)
                {
                    return true;
                }

                var ownerId = context.Properties.Where(p => p.Id == x.PropertyId)
                    .Select(p => (int?)p.OwnerId).FirstOrDefault();
                return ownerId == user.Id;
            })
            .WithMessage("You are not allowed to modify amenities for this property");
    }
}
