using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AddCountryValidator : AbstractValidator<AddCountryDTO>
{
    public AddCountryValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        
        RuleFor(x=>x.Name).
            Must(x=>!context.Countries.Any(c=>c.Name==x))
            .WithMessage("Country already exists.");
    }
}