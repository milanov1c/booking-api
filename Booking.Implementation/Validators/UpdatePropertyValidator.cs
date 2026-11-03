using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class UpdatePropertyValidator : AbstractValidator<UpdatePropertyDTO>
{
    public UpdatePropertyValidator(BookingDbContext context, IApplicationUser user)
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
            .WithMessage("You are not allowed to update this property");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");

        RuleFor(x => x)
            .Must(x => !context.Properties.Any(p => p.Name == x.Name && p.Id != x.Id))
            .WithMessage("Property with that name already exists");

        RuleFor(x => x.DestinationId)
            .Must(id => context.Destinations.Any(d => d.Id == id))
            .WithMessage("Destination doesn't exist");

        RuleFor(x => x.PropertyTypeId)
            .Must(id => context.PropertyTypes.Any(pt => pt.Id == id))
            .WithMessage("Property type doesn't exist");
    }
}
