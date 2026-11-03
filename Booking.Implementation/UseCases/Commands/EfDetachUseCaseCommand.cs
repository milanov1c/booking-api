using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfDetachUseCaseCommand : EfUseCase, IDetachUseCaseCommand
{
    private readonly DetachUseCaseValidator _validator;

    public EfDetachUseCaseCommand(BookingDbContext context, DetachUseCaseValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(DetachUseCaseDTO request)
    {
        _validator.ValidateAndThrow(request);

        var roleUseCase = ctx.RoleUseCases
            .First(ru => ru.RoleId == request.RoleId && ru.UseCaseId == request.UseCaseId);

        ctx.RoleUseCases.Remove(roleUseCase);
        ctx.SaveChanges();
    }

    public string Name => "Detach Use Case";
    public string Id => "ef-detach-use-case";
}
