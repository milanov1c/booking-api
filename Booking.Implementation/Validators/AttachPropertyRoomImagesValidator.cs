using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AttachPropertyRoomImagesValidator : AbstractValidator<AttachPropertyRoomImagesDTO>
{
    private static readonly string[] AllowedContentTypes =
    {
        "image/jpeg",
        "image/png"
    };

    private const long MaxImageSizeBytes = 5 * 1024 * 1024; // 5 MB

    public AttachPropertyRoomImagesValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.PropertyRoomId)
            .Must(id => context.PropertyRooms.Any(r => r.Id == id))
            .WithMessage("Property room doesn't exist");

        RuleFor(x => x.Images)
            .NotEmpty().WithMessage("At least one image is required");

        RuleForEach(x => x.Images)
            .ChildRules(image =>
            {
                image.RuleFor(i => i.Length)
                    .GreaterThan(0).WithMessage("Uploaded image is empty")
                    .LessThanOrEqualTo(MaxImageSizeBytes).WithMessage("Image must not exceed 5 MB");

                image.RuleFor(i => i.ContentType)
                    .Must(contentType => AllowedContentTypes.Contains(contentType?.ToLowerInvariant()))
                    .WithMessage("Image must be one of: " + string.Join(", ", AllowedContentTypes));
            });
    }
}
