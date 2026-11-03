using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AddPropertyValidator : AbstractValidator<AddPropertyDTO>
{
    private static readonly string[] AllowedContentTypes =
    {
        "image/jpeg",
        "image/png"
    };

    private const long MaxImageSizeBytes = 5 * 1024 * 1024; // 5 MB

    public AddPropertyValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");

        RuleFor(x => x.Name)
            .Must(name => !context.Properties.Any(p => p.Name == name))
            .WithMessage("Property with that name already exists");

        RuleFor(x => x.DestinationId)
            .Must(id => context.Destinations.Any(d => d.Id == id))
            .WithMessage("Destination doesn't exist");

        RuleFor(x => x.PropertyTypeId)
            .Must(id => context.PropertyTypes.Any(pt => pt.Id == id))
            .WithMessage("Property type doesn't exist");

        // Admins are allowed to own properties too, so they can manage
        // everything a Landlord can without needing a separate landlord
        // account (e.g. when creating a property through the shared
        // landlord UI while logged in as Admin).
        RuleFor(x => x.LandlordId)
            .Must(id => context.Users.Any(u => u.Id == id && (u.Role.Name == "Landlord" || u.Role.Name == "Admin")))
            .WithMessage("Landlord doesn't exist");

        RuleFor(x => x.Image)
            .NotNull().WithMessage("Property image is required");

        When(x => x.Image != null, () =>
        {
            RuleFor(x => x.Image.Length)
                .GreaterThan(0).WithMessage("Uploaded image is empty")
                .LessThanOrEqualTo(MaxImageSizeBytes).WithMessage("Image must not exceed 5 MB");

            RuleFor(x => x.Image.ContentType)
                .Must(contentType => AllowedContentTypes.Contains(contentType?.ToLowerInvariant()))
                .WithMessage("Image must be one of: " + string.Join(", ", AllowedContentTypes));
        });
    }
}
