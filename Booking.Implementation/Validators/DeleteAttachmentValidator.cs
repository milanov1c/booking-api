using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class DeleteAttachmentValidator : AbstractValidator<DeleteAttachmentDTO>
{
    public DeleteAttachmentValidator(BookingDbContext context, IApplicationUser user)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .Must(id => context.Attachments.Any(a => a.Id == id))
            .WithMessage("Attachment doesn't exist")
            .Must(id =>
            {
                var isAdmin = string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase);
                if (isAdmin)
                {
                    return true;
                }

                var ownerId = context.Attachments
                    .Where(a => a.Id == id)
                    .Select(a => (int?)(a.PropertyId != null
                        ? a.Property.OwnerId
                        : a.PropertyRoom.Property.OwnerId))
                    .FirstOrDefault();

                return ownerId == user.Id;
            })
            .WithMessage("You are not allowed to delete this image")
            .Must(id =>
            {
                // Don't allow removing the last remaining image of a
                // property/property room - a listing should always have at
                // least one photo (matches AddPropertyValidator requiring one).
                var attachment = context.Attachments.First(a => a.Id == id);

                if (attachment.PropertyId != null)
                {
                    return context.Attachments.Count(a => a.PropertyId == attachment.PropertyId) > 1;
                }

                return context.Attachments.Count(a => a.PropertyRoomId == attachment.PropertyRoomId) > 1;
            })
            .WithMessage("Cannot remove the last remaining image");
    }
}
