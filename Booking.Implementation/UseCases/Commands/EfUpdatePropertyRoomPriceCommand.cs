using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfUpdatePropertyRoomPriceCommand : EfUseCase, IUpdatePropertyRoomPriceCommand
{
    private readonly UpdatePropertyRoomPriceValidator _validator;

    public EfUpdatePropertyRoomPriceCommand(BookingDbContext context, UpdatePropertyRoomPriceValidator validator)
        : base(context)
    {
        _validator = validator;
    }

    public void Execute(UpdatePropertyRoomPriceDTO request)
    {
        _validator.ValidateAndThrow(request);

        var price = ctx.PropertyRoomPrices.First(p => p.Id == request.Id);

        price.Price = request.Price;
        price.Date = request.Date.Date.ToUniversalTime();
        price.UpdatedAt = DateTime.UtcNow;

        ctx.SaveChanges();
    }

    public string Name => "Update Property Room Price";
    public string Id => "ef-update-property-room-price";
}
