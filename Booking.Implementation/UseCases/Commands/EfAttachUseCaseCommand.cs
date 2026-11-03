using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfAttachUseCaseCommand : EfUseCase, IAttachUseCaseCommand
{
    private AttachUseCaseValidator _validator;
    public EfAttachUseCaseCommand(BookingDbContext context, AttachUseCaseValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(AttachUseCaseDTO request)
    {
        _validator.ValidateAndThrow(request);
        RoleUseCase roleUseCase = new RoleUseCase();
        roleUseCase.UseCaseId=request.UseCaseId;
        roleUseCase.RoleId=request.RoleId;
        ctx.RoleUseCases.Add(roleUseCase);
        ctx.SaveChanges();

    }

    public string Name => "Attach use case";
    public string Id => "ef-attach-use-case";
}