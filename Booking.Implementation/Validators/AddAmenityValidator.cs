using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AddAmenityValidator : AbstractValidator<AddAmenityDTO>
{
    public AddAmenityValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Name)
            .Must(name => !context.Amenities.Any(a => a.Name == name))
            .WithMessage("Amenity with that name already exists");
    }
}
