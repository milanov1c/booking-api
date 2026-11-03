using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfAttachPriceCommand : EfUseCase, IAttachPriceCommand
{
    private AttachPriceValidator _validator;
    public EfAttachPriceCommand(BookingDbContext context, AttachPriceValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(AttachPriceDTO request)
    {
        _validator.ValidateAndThrow(request);
        
        var startDate = request.StartDate.Date;
        var endDate= (request.EndDate ?? request.StartDate).Date;
        var prices = new List<PropertyRoomPrice>();

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            prices.Add(new PropertyRoomPrice
            {
                PropertyRoomId = request.PropertyRoomId,
                Price =  request.Price,
                Date =  date.ToUniversalTime()
            });
        }
        
        ctx.AddRange(prices);
        ctx.SaveChanges();
    }

    public string Name => "Attach Price";
    public string Id => "ef-attach-price";
}