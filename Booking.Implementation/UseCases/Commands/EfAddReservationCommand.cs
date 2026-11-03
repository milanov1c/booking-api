using Booking.Application;
using Booking.Application.Commands;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using Booking.Implementation.Validators;
using FluentValidation;

namespace Booking.Implementation.UseCases.Commands;

public class EfAddReservationCommand : EfUseCase, IAddReservationCommand
{
    private readonly AddReservationValidator _validator;
    private readonly IApplicationUser _user;

    public EfAddReservationCommand(BookingDbContext context, AddReservationValidator validator, IApplicationUser user)
        : base(context)
    {
        _validator = validator;
        _user = user;
    }

    public void Execute(AddReservationDTO request)
    {
        request.StartDate=DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
        request.EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);
        _validator.ValidateAndThrow(request);

        var startDate = request.StartDate.Date;
        var endDate = request.EndDate.Date;

        var totalPrice = ctx.PropertyRoomPrices
            .Where(p => p.PropertyRoomId == request.PropertyRoomId
                        && p.Date.Date >= startDate
                        && p.Date.Date < endDate)
            .Sum(p => p.Price);

       
        Reservation reservation = new Reservation
        {
            UserId = _user.Id,
            PropertyRoomId = request.PropertyRoomId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalPrice = totalPrice,
            Status = ReservationStatus.Pending,
        };

        ctx.Reservations.Add(reservation);
        ctx.SaveChanges();
    }

    public string Name => "Add Reservation";
    public string Id => "ef-add-reservation";
}
