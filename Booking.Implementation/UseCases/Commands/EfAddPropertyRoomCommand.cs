using Booking.Application;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Booking.Implementation.UseCases.Commands;

public class EfAddPropertyRoomCommand : EfUseCase, IAddPropertyRoomCommand
{
    private readonly AddPropertyRoomValidator _validator;
    private readonly IFileStorage _fileStorage;

    public EfAddPropertyRoomCommand(BookingDbContext context, AddPropertyRoomValidator validator, IFileStorage fileStorage)
        : base(context)
    {
        _validator = validator;
        _fileStorage = fileStorage;
    }

    public void Execute(AddPropertyRoomDTO request)
    {
        _validator.ValidateAndThrow(request);

        using var transaction = ctx.Database.BeginTransaction();

        PropertyRoom propertyRoom = new PropertyRoom
        {
            Name = request.Name,
            Capacity = request.Capacity,
            PropertyId = request.PropertyId,
        };

        ctx.PropertyRooms.Add(propertyRoom);
        ctx.SaveChanges();

        var storedPath = _fileStorage.Save(request.Image, "property-rooms");

        Attachment attachment = new Attachment
        {
            Path = storedPath,
            PropertyRoomId = propertyRoom.Id,
        };

        ctx.Attachments.Add(attachment);
        ctx.SaveChanges();

        transaction.Commit();
    }

    public string Name => "Add Property Room";
    public string Id => "ef-add-property-room";
}
