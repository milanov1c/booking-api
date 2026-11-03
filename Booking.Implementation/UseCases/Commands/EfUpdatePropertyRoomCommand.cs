using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfUpdatePropertyRoomCommand : EfUseCase, IUpdatePropertyRoomCommand
{
    private readonly UpdatePropertyRoomValidator _validator;

    public EfUpdatePropertyRoomCommand(BookingDbContext context, UpdatePropertyRoomValidator validator)
        : base(context)
    {
        _validator = validator;
    }

    public void Execute(UpdatePropertyRoomDTO request)
    {
        _validator.ValidateAndThrow(request);

        var room = ctx.PropertyRooms.First(r => r.Id == request.Id);

        room.Name = request.Name;
        room.Capacity = request.Capacity;
        room.UpdatedAt = DateTime.UtcNow;

        ctx.SaveChanges();
    }

    public string Name => "Update Property Room";
    public string Id => "ef-update-property-room";
}
