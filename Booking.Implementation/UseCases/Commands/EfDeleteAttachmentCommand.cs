using Booking.Application;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfDeleteAttachmentCommand : EfUseCase, IDeleteAttachmentCommand
{
    private readonly DeleteAttachmentValidator _validator;
    private readonly IFileStorage _fileStorage;

    public EfDeleteAttachmentCommand(BookingDbContext context, DeleteAttachmentValidator validator, IFileStorage fileStorage)
        : base(context)
    {
        _validator = validator;
        _fileStorage = fileStorage;
    }

    public void Execute(DeleteAttachmentDTO request)
    {
        _validator.ValidateAndThrow(request);

        var attachment = ctx.Attachments.First(a => a.Id == request.Id);
        var path = attachment.Path;

        ctx.Attachments.Remove(attachment);
        ctx.SaveChanges();

        _fileStorage.Delete(path);
    }

    public string Name => "Delete Attachment";
    public string Id => "ef-delete-attachment";
}
