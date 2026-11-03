using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfConfirmReservation : EfUseCase, IConfirmReservationCommand
{
    private ConfirmReservationValidator _validator;
    public EfConfirmReservation(BookingDbContext context, ConfirmReservationValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(ConfirmReservationDTO request)
    {
        _validator.ValidateAndThrow(request);
        Reservation reservation = ctx.Reservations.Find(request.ReservationId);
        reservation.Status = ReservationStatus.Confirmed;
        reservation.UpdatedAt = DateTime.UtcNow;
        ctx.SaveChanges();
    }

    public string Name => "Confirm Reservation";
    public string Id => "ef-confirm-reservation";
}