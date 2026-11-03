using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AddReservationValidator : AbstractValidator<AddReservationDTO>
{
    public AddReservationValidator(BookingDbContext context, IApplicationUser user)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.PropertyRoomId)
            .Must(id => context.PropertyRooms.Any(r => r.Id == id))
            .WithMessage("Property room doesn't exist")
            .Must(id => context.PropertyRooms
                .Where(r => r.Id == id)
                .Select(r => (int?)r.Property.OwnerId)
                .FirstOrDefault() != user.Id)
            .WithMessage("Landlord cannot make a reservation for their own property");

        RuleFor(x => x.StartDate)
            .Must(date => date.Date >= DateTime.UtcNow.Date)
            .WithMessage("Start date cannot be in the past");

        RuleFor(x => x)
            .Must(x => x.EndDate.Date > x.StartDate.Date)
            .WithMessage("End date must be after start date");
        
        When(x => x.EndDate.Date > x.StartDate.Date, () =>
        {
            RuleFor(x => x)
                .Must(x => !context.Reservations.Any(r =>
                    r.PropertyRoomId == x.PropertyRoomId &&
                    r.Status != ReservationStatus.Cancelled &&
                    r.StartDate.Date < x.EndDate.Date &&
                    r.EndDate.Date > x.StartDate.Date))
                .WithMessage("Property room is already booked for one or more of the requested nights");
        
            RuleFor(x => x)
                .Must(x =>
                {
                    var nights = (x.EndDate.Date - x.StartDate.Date).Days;
        
                    var pricedNights = context.PropertyRoomPrices.Count(p =>
                        p.PropertyRoomId == x.PropertyRoomId &&
                        p.Date.Date >= x.StartDate.Date &&
                        p.Date.Date < x.EndDate.Date);
        
                    return pricedNights == nights;
                })
                .WithMessage("Price is not set for one or more nights in the requested range");
        });
    }
}
