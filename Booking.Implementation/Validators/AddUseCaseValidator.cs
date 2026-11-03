using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AddUseCaseValidator : AbstractValidator<AddUseCaseDTO>
{
    public AddUseCaseValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode=CascadeMode.Stop;
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x=>x.UseCaseId).NotEmpty().WithMessage("Use case id is required");
        
        RuleFor(x=>x.UseCaseId)
            .Must(x=>!context.UseCases.Any(u=>u.UseCaseId==x))
            .WithMessage("Use Case with that id already exists");
    }
}