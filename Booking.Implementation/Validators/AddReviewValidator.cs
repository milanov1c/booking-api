using Booking.Application;
using Booking.Application.DTO;
using Booking.DataAccess;
using Booking.Domain;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AddReviewValidator : AbstractValidator<AddReviewDTO>
{
    public AddReviewValidator(BookingDbContext context, IApplicationUser user)
    {
        RuleFor(x => x.PropertyId).GreaterThan(0).WithMessage("Property id is required");
        
        RuleFor(x=>x.Rating).GreaterThan(0).WithMessage("Rating must be greater than 0")
                                            .LessThan(6).WithMessage("Rating must be less than 6");
        
        RuleFor(x=>x.Comment).NotEmpty().WithMessage("Comment is required");
        
        RuleFor(x=>x.PropertyId)
            .Must(x=> context.Properties.Any(p=>p.Id==x)).WithMessage("Property doesnt exist");
        
        RuleFor(x=>x)
            .Must(x=>context.Reservations
                .Any(r=>r.PropertyRoom.PropertyId==x.PropertyId && r.UserId==user.Id && r.Status==ReservationStatus.Confirmed))
            .WithMessage("Reservation doesnt exist")
            .Must(x => context.Reservations
                .Any(r => r.PropertyRoom.PropertyId == x.PropertyId
                    && r.UserId == user.Id
                    && r.Status == ReservationStatus.Confirmed
                    && r.EndDate.Date <= DateTime.UtcNow.Date))
            .WithMessage("You can only review a property after your stay has ended");
    }
}