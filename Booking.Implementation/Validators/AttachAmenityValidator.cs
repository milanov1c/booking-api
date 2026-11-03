using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AttachAmenityValidator : AbstractValidator<AttachAmenityDTO>
{
    public AttachAmenityValidator(BookingDbContext context)
    {
        RuleFor(x=>x.AmenityId).GreaterThan(0).WithMessage("Amenity id is required")
            .Must(x=> context.Amenities.Any(a=>a.Id==x)).WithMessage("Amenity id does not exist");
        
        RuleFor(z=>z.PropertyId).GreaterThan(0).WithMessage("Property id is required")
            .Must(x=> context.Properties.Any(a=>a.Id==x)).WithMessage("Property id does not exist");
        
        RuleFor(x=>x)
            .Must(x=> !context.AmenityProperties
                .Any(a=>a.AmenityId==x.AmenityId && a.PropertyId==x.PropertyId))
            .WithMessage("Amenity already attached");
    }
}