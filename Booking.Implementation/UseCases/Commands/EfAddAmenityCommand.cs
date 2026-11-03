using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfAddAmenityCommand : EfUseCase, IAddAmenityCommand
{
    private readonly AddAmenityValidator _validator;

    public EfAddAmenityCommand(BookingDbContext context, AddAmenityValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(AddAmenityDTO request)
    {
        _validator.ValidateAndThrow(request);

        Amenity amenity = new Amenity
        {
            Name = request.Name,
        };

        ctx.Amenities.Add(amenity);
        ctx.SaveChanges();
    }

    public string Name => "Add Amenity";
    public string Id => "ef-add-amenity";
}
