using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class UpdatePropertyRoomPriceValidator : AbstractValidator<UpdatePropertyRoomPriceDTO>
{
    public UpdatePropertyRoomPriceValidator(BookingDbContext context, IApplicationUser user)
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .Must(id => context.PropertyRoomPrices.Any(p => p.Id == id))
            .WithMessage("Price entry doesn't exist")
            .Must(id =>
            {
                var isAdmin = string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase);
                if (isAdmin)
                {
                    return true;
                }

                var ownerId = context.PropertyRoomPrices.Where(p => p.Id == id)
                    .Select(p => (int?)p.PropertyRoom.Property.OwnerId).FirstOrDefault();
                return ownerId == user.Id;
            })
            .WithMessage("You are not allowed to update this price");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price is required");

        RuleFor(x => x)
            .Must(x =>
            {
                var date = x.Date.Date.ToUniversalTime();
                var propertyRoomId = context.PropertyRoomPrices
                    .Where(p => p.Id == x.Id)
                    .Select(p => p.PropertyRoomId)
                    .FirstOrDefault();

                return !context.PropertyRoomPrices.Any(p =>
                    p.Id != x.Id &&
                    p.PropertyRoomId == propertyRoomId &&
                    p.Date == date);
            })
            .WithMessage("A price already exists for this property room on that date")
            .Must(x =>
            {
                var date = x.Date.Date.ToUniversalTime();
                var propertyRoomId = context.PropertyRoomPrices
                    .Where(p => p.Id == x.Id)
                    .Select(p => p.PropertyRoomId)
                    .FirstOrDefault();

                return !context.Reservations.Any(r =>
                    r.PropertyRoomId == propertyRoomId &&
                    r.Status != ReservationStatus.Cancelled &&
                    date >= r.StartDate.Date &&
                    date < r.EndDate.Date);
            })
            .WithMessage("Cannot move this price to a date that's already covered by a reservation");
    }
}
