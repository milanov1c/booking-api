using Booking.Application;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfAttachPropertyImagesCommand : EfUseCase, IAttachPropertyImagesCommand
{
    private readonly AttachPropertyImagesValidator _validator;
    private readonly IFileStorage _fileStorage;

    public EfAttachPropertyImagesCommand(BookingDbContext context, AttachPropertyImagesValidator validator, IFileStorage fileStorage)
        : base(context)
    {
        _validator = validator;
        _fileStorage = fileStorage;
    }

    public void Execute(AttachPropertyImagesDTO request)
    {
        _validator.ValidateAndThrow(request);

        using var transaction = ctx.Database.BeginTransaction();

        var attachments = request.Images
            .Select(image => new Attachment
            {
                Path = _fileStorage.Save(image, "properties"),
                PropertyId = request.PropertyId,
            })
            .ToList();

        ctx.Attachments.AddRange(attachments);
        ctx.SaveChanges();

        transaction.Commit();
    }

    public string Name => "Attach Property Images";
    public string Id => "ef-attach-property-images";
}
