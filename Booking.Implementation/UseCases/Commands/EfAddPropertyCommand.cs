using Booking.Application;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Booking.Implementation.UseCases.Commands;

public class EfAddPropertyCommand : EfUseCase, IAddPropertyCommand
{
    private readonly AddPropertyValidator _validator;
    private readonly IFileStorage _fileStorage;

    public EfAddPropertyCommand(BookingDbContext context, AddPropertyValidator validator, IFileStorage fileStorage)
        : base(context)
    {
        _validator = validator;
        _fileStorage = fileStorage;
    }

    public void Execute(AddPropertyDTO request)
    {
        _validator.ValidateAndThrow(request);

        using var transaction = ctx.Database.BeginTransaction();

        Property property = new Property
        {
            Name = request.Name,
            Description = request.Description,
            Address = request.Address,
            PropertyTypeId = request.PropertyTypeId,
            DestinationId = request.DestinationId,
            OwnerId = request.LandlordId,
        };

        ctx.Properties.Add(property);
        ctx.SaveChanges();

        var storedPath = _fileStorage.Save(request.Image, "properties");

        Attachment attachment = new Attachment
        {
            Path = storedPath,
            PropertyId = property.Id,
        };

        ctx.Attachments.Add(attachment);
        ctx.SaveChanges();

        transaction.Commit();
    }

    public string Name => "Add Property";
    public string Id => "ef-add-property";
}
