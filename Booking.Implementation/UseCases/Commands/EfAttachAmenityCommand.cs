using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfAttachAmenityCommand : EfUseCase, IAttachAmenityCommand
{
    private AttachAmenityValidator _validator;
    public EfAttachAmenityCommand(BookingDbContext context, AttachAmenityValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(AttachAmenityDTO request)
    {
        _validator.ValidateAndThrow(request);
        AmenityProperty amenityProperty = new AmenityProperty();
        amenityProperty.AmenityId = request.AmenityId;
        amenityProperty.PropertyId = request.PropertyId;
        ctx.AmenityProperties.Add(amenityProperty);
        ctx.SaveChanges();
    }

    public string Name => "Attach Amenity";
    public string Id => "ef-attach-amenity";
}