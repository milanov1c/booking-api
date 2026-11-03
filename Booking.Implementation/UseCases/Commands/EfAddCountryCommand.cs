using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfAddCountryCommand : EfUseCase, IAddCountryCommand
{
    private AddCountryValidator _validator;
    public EfAddCountryCommand(BookingDbContext context, AddCountryValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(AddCountryDTO request)
    {
        _validator.ValidateAndThrow(request);
        
        Country country = new Country();
        country.Name = request.Name;
        ctx.Add(country);
        ctx.SaveChanges();
    }

    public string Name => "Add Country";
    public string Id => "ef-add-country";
}