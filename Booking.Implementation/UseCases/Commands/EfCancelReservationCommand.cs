using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfCancelReservationCommand : EfUseCase, ICancelReservationCommand
{
    private CancelReservationValidator _validator;
    public EfCancelReservationCommand(BookingDbContext context, CancelReservationValidator validator) : base(context)
    {
        _validator = validator;
    }

    public void Execute(CancelReservationDTO request)
    {
        _validator.ValidateAndThrow(request);
        Reservation reservation=ctx.Reservations.Find(request.ReservationId);
        reservation.Status = ReservationStatus.Cancelled;
        reservation.UpdatedAt = DateTime.UtcNow;
        ctx.SaveChanges();
    }

    public string Name => "Cancel Reservation";
    public string Id => "ef-cancel-reservation";
}