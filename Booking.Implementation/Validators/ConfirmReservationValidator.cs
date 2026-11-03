using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class ConfirmReservationValidator : AbstractValidator<ConfirmReservationDTO>
{
    public ConfirmReservationValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode=CascadeMode.Stop;
        RuleFor(x => x.ReservationId)
            .NotEmpty()
            .WithMessage("Reservation Id cannot be empty");
        
        RuleFor(x=>x)
            .Must(x=> context.Reservations.Any(r=>r.Id==x.ReservationId))
            .WithMessage("Reservation doesn't exist");

        RuleFor(x => x)
            .Must(x => context.Reservations
                .Any(r => r.Id == x.ReservationId && r.Status == ReservationStatus.Pending))
            .WithMessage("Reservation is already confirmed");
    }
}