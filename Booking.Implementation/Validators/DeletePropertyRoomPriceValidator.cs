using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class DeletePropertyRoomPriceValidator : AbstractValidator<DeletePropertyRoomPriceDTO>
{
    public DeletePropertyRoomPriceValidator(BookingDbContext context, IApplicationUser user)
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
            .WithMessage("You are not allowed to delete this price")
            .Must(id =>
            {
                var price = context.PropertyRoomPrices.First(p => p.Id == id);

                return !context.Reservations.Any(r =>
                    r.PropertyRoomId == price.PropertyRoomId &&
                    r.Status != ReservationStatus.Cancelled &&
                    price.Date.Date >= r.StartDate.Date &&
                    price.Date.Date < r.EndDate.Date);
            })
            .WithMessage("Cannot delete a price for a date that's already covered by a reservation");
    }
}
