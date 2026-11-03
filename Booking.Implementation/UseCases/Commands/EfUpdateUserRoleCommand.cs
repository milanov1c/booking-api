using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfUpdateUserRoleCommand : EfUseCase, IUpdateUserRoleCommand
{
    private readonly UpdateUserRoleValidator _validator;

    public EfUpdateUserRoleCommand(BookingDbContext context, UpdateUserRoleValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(UpdateUserRoleDTO request)
    {
        _validator.ValidateAndThrow(request);

        var user = ctx.Users.First(u => u.Id == request.UserId);

        user.RoleId = request.RoleId;
        user.UpdatedAt = DateTime.UtcNow;

        ctx.SaveChanges();
    }

    public string Name => "Update User Role";
    public string Id => "ef-update-user-role";
}
