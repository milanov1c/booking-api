using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfAddRoleCommand : EfUseCase, IAddRoleCommand
{
    private RoleValidator _roleValidator;
    public EfAddRoleCommand(BookingDbContext context, RoleValidator validator) : base(context)
    {
        _roleValidator = validator;
    }

    public void Execute(RoleDTO request)
    {
        _roleValidator.ValidateAndThrow(request);
        Role role = new Role();
        role.Name = request.Name;
        ctx.Add(role);
        ctx.SaveChanges();
    }

    public string Name =>"Add Role";
    public string Id =>"ef-add-role";
}