using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfUpdateUserStatusCommand : EfUseCase, IUpdateUserStatusCommand
{
    private readonly UpdateUserStatusValidator _validator;

    public EfUpdateUserStatusCommand(BookingDbContext context, UpdateUserStatusValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(UpdateUserStatusDTO request)
    {
        _validator.ValidateAndThrow(request);

        var user = ctx.Users.First(u => u.Id == request.UserId);

        user.IsActive = request.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        ctx.SaveChanges();
    }

    public string Name => "Update User Status";
    public string Id => "ef-update-user-status";
}
