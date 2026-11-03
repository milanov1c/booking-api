using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfDetachAmenityCommand : EfUseCase, IDetachAmenityCommand
{
    private readonly DetachAmenityValidator _validator;

    public EfDetachAmenityCommand(BookingDbContext context, DetachAmenityValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(DetachAmenityDTO request)
    {
        _validator.ValidateAndThrow(request);

        var amenityProperty = ctx.AmenityProperties
            .First(a => a.AmenityId == request.AmenityId && a.PropertyId == request.PropertyId);

        ctx.AmenityProperties.Remove(amenityProperty);
        ctx.SaveChanges();
    }

    public string Name => "Detach Amenity";
    public string Id => "ef-detach-amenity";
}
