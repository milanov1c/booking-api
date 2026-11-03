using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfAddDestinationCommand : EfUseCase, IAddDestinationCommand
{
    private AddDestinationValidator _validator;
    public EfAddDestinationCommand(BookingDbContext context, AddDestinationValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(AddDestinationDTO request)
    {
        _validator.ValidateAndThrow(request);
        Destination destination = new Destination();
        destination.City = request.Name;
        destination.CountryId = request.CountryId;
        ctx.Destinations.Add(destination);
        ctx.SaveChanges();
    }

    public string Name => "Add Destination";
    public string Id => "ef-add-destination";
}