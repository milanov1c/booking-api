using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AttachUseCaseValidator : AbstractValidator<AttachUseCaseDTO>
{
    public AttachUseCaseValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode=CascadeMode.Stop;
        RuleFor(x => x.UseCaseId)
            .NotEmpty().WithMessage("UseCaseId is required");
        RuleFor(x=>x.RoleId)
            .NotEmpty().WithMessage("RoleId is required");
        
        RuleFor(x => x.RoleId)
            .Must((dto, roleId) =>
            {
                return !context.RoleUseCases
                    .Any(x => x.RoleId == roleId 
                              && x.UseCaseId == dto.UseCaseId);
            })
            .WithMessage("Role already has this use case.");
    }
}