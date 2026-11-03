using Booking.Application;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Booking.Implementation.UseCases.Commands;

public class EfDeletePropertyRoomCommand : EfUseCase, IDeletePropertyRoomCommand
{
    private readonly DeletePropertyRoomValidator _validator;
    private readonly IFileStorage _fileStorage;

    public EfDeletePropertyRoomCommand(BookingDbContext context, DeletePropertyRoomValidator validator, IFileStorage fileStorage)
        : base(context)
    {
        _validator = validator;
        _fileStorage = fileStorage;
    }

    public void Execute(DeletePropertyRoomDTO request)
    {
        _validator.ValidateAndThrow(request);

        var room = ctx.PropertyRooms
            .Include(r => r.Attachments)
            .First(r => r.Id == request.Id);

        var pathsToDelete = room.Attachments.Select(a => a.Path).ToList();

        using var transaction = ctx.Database.BeginTransaction();

        ctx.PropertyRooms.Remove(room);
        ctx.SaveChanges();

        transaction.Commit();

        foreach (var path in pathsToDelete)
        {
            _fileStorage.Delete(path);
        }
    }

    public string Name => "Delete Property Room";
    public string Id => "ef-delete-property-room";
}
