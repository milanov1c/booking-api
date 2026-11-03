using Booking.Application;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Booking.Implementation.UseCases.Commands;

public class EfDeletePropertyCommand : EfUseCase, IDeletePropertyCommand
{
    private readonly DeletePropertyValidator _validator;
    private readonly IFileStorage _fileStorage;

    public EfDeletePropertyCommand(BookingDbContext context, DeletePropertyValidator validator, IFileStorage fileStorage)
        : base(context)
    {
        _validator = validator;
        _fileStorage = fileStorage;
    }

    public void Execute(DeletePropertyDTO request)
    {
        _validator.ValidateAndThrow(request);

        var property = ctx.Properties
            .Include(p => p.Attachments)
            .Include(p => p.PropertyRooms)
                .ThenInclude(r => r.Attachments)
            .First(p => p.Id == request.Id);

        var pathsToDelete = property.Attachments.Select(a => a.Path)
            .Concat(property.PropertyRooms.SelectMany(r => r.Attachments).Select(a => a.Path))
            .ToList();

        using var transaction = ctx.Database.BeginTransaction();

        ctx.Properties.Remove(property);
        ctx.SaveChanges();

        transaction.Commit();

        foreach (var path in pathsToDelete)
        {
            _fileStorage.Delete(path);
        }
    }

    public string Name => "Delete Property";
    public string Id => "ef-delete-property";
}
