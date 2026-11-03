using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AddDestinationValidator : AbstractValidator<AddDestinationDTO>
{
    public AddDestinationValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode=CascadeMode.Stop;
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.CountryId).NotEmpty().WithMessage("CountryId is required");
        RuleFor(x=>x.Name)
                    .Must(x=>!context.Destinations.Any(d=>d.City==x))
                    .WithMessage("Destination already exists");
        RuleFor(x=>x.CountryId)
            .Must(x=>context.Countries.Any(c=>c.Id==x))
            .WithMessage("Country doesnt exist");
    }
}
