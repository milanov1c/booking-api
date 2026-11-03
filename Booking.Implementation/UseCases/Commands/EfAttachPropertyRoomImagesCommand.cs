using Booking.Application;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Booking.Implementation.UseCases.Commands;

public class EfAttachPropertyRoomImagesCommand : EfUseCase, IAttachPropertyRoomImagesCommand
{
    private readonly AttachPropertyRoomImagesValidator _validator;
    private readonly IFileStorage _fileStorage;

    public EfAttachPropertyRoomImagesCommand(BookingDbContext context, AttachPropertyRoomImagesValidator validator, IFileStorage fileStorage)
        : base(context)
    {
        _validator = validator;
        _fileStorage = fileStorage;
    }

    public void Execute(AttachPropertyRoomImagesDTO request)
    {
        _validator.ValidateAndThrow(request);

        using var transaction = ctx.Database.BeginTransaction();

        var attachments = request.Images
            .Select(image => new Attachment
            {
                Path = _fileStorage.Save(image, "property-rooms"),
                PropertyRoomId = request.PropertyRoomId,
            })
            .ToList();

        ctx.Attachments.AddRange(attachments);
        ctx.SaveChanges();

        transaction.Commit();
    }

    public string Name => "Attach Property Room Images";
    public string Id => "ef-attach-property-room-images";
}
