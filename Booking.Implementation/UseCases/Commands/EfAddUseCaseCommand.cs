using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfAddUseCaseCommand : EfUseCase, IAddUseCaseCommand
{
    private AddUseCaseValidator _validator;
    public EfAddUseCaseCommand(BookingDbContext context, AddUseCaseValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(AddUseCaseDTO request)
    {
        _validator.ValidateAndThrow(request);
        UseCase useCase = new UseCase();
        useCase.Name = request.Name;
        useCase.UseCaseId = request.UseCaseId;
        ctx.UseCases.Add(useCase);
        ctx.SaveChanges();
    }

    public string Name => "Add Use Case";
    public string Id => "ef-add-use-case";
}