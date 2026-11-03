using Booking.Application;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfChangePasswordCommand : EfUseCase, IChangePasswordCommand
{
    private readonly ChangePasswordValidator _validator;
    private readonly IApplicationUser _user;

    public EfChangePasswordCommand(BookingDbContext context, ChangePasswordValidator validator, IApplicationUser user)
        : base(context)
    {
        _validator = validator;
        _user = user;
    }

    public void Execute(ChangePasswordDTO request)
    {
        _validator.ValidateAndThrow(request);

        var user = ctx.Users.First(u => u.Id == _user.Id);

        user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        ctx.SaveChanges();
    }

    public string Name => "Change Password";
    public string Id => "ef-change-password";
}
