using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfDeletePropertyRoomPriceCommand : EfUseCase, IDeletePropertyRoomPriceCommand
{
    private readonly DeletePropertyRoomPriceValidator _validator;

    public EfDeletePropertyRoomPriceCommand(BookingDbContext context, DeletePropertyRoomPriceValidator validator)
        : base(context)
    {
        _validator = validator;
    }

    public void Execute(DeletePropertyRoomPriceDTO request)
    {
        _validator.ValidateAndThrow(request);

        var price = ctx.PropertyRoomPrices.First(p => p.Id == request.Id);

        ctx.PropertyRoomPrices.Remove(price);
        ctx.SaveChanges();
    }

    public string Name => "Delete Property Room Price";
    public string Id => "ef-delete-property-room-price";
}
