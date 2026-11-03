using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class CancelReservationValidator : AbstractValidator<CancelReservationDTO>
{
    public CancelReservationValidator(BookingDbContext context)
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
                .Any(r => r.Id == x.ReservationId && r.Status == ReservationStatus.Pending || r.Status == ReservationStatus.Confirmed))
            .WithMessage("Reservation is already cancelled");
    }
}