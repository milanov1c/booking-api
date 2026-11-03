using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfUpdatePropertyCommand : EfUseCase, IUpdatePropertyCommand
{
    private readonly UpdatePropertyValidator _validator;

    public EfUpdatePropertyCommand(BookingDbContext context, UpdatePropertyValidator validator)
        : base(context)
    {
        _validator = validator;
    }

    public void Execute(UpdatePropertyDTO request)
    {
        _validator.ValidateAndThrow(request);

        var property = ctx.Properties.First(p => p.Id == request.Id);

        property.Name = request.Name;
        property.Description = request.Description;
        property.Address = request.Address;
        property.PropertyTypeId = request.PropertyTypeId;
        property.DestinationId = request.DestinationId;
        property.UpdatedAt = DateTime.UtcNow;

        ctx.SaveChanges();
    }

    public string Name => "Update Property";
    public string Id => "ef-update-property";
}
