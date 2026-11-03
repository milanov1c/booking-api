using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AddPropertyRoomValidator : AbstractValidator<AddPropertyRoomDTO>
{
    private static readonly string[] AllowedContentTypes =
    {
        "image/jpeg",
        "image/png"
    };

    private const long MaxImageSizeBytes = 5 * 1024 * 1024; // 5 MB

    public AddPropertyRoomValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.PropertyId)
            .Must(id => context.Properties.Any(p => p.Id == id))
            .WithMessage("Property doesn't exist");

        // Room names only need to be unique within the same property - two
        // different properties (e.g. owned by different landlords) should
        // both be able to have a "Room 1" or "Standard Room".
        RuleFor(x => x)
            .Must(x => !context.PropertyRooms.Any(r => r.Name == x.Name && r.PropertyId == x.PropertyId))
            .WithMessage("Property room with that name already exists for this property")
            .OverridePropertyName("Name");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than 0");

        RuleFor(x => x.Image)
            .NotNull().WithMessage("Property room image is required");

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
