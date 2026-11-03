using System.Windows.Input;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
{
    private RegisterUserValidator _validator;
    public EfRegisterUserCommand(BookingDbContext context, RegisterUserValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(RegisterUserDTO request)
    {
        _validator.ValidateAndThrow(request);
        
        string hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        User user = new User();
        user.Password = hash;
        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.RoleId = request.RoleId;

        ctx.Users.Add(user);
        ctx.SaveChanges();
    }

    public string Name => "Register user";
    public string Id => "ef-register-user";
}