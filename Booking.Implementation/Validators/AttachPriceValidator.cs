using System.ComponentModel;
using Booking.Application.DTO;
using Booking.DataAccess;
using FluentValidation;

namespace Booking.Implementation.Validators;

public class AttachPriceValidator : AbstractValidator<AttachPriceDTO>
{
    public AttachPriceValidator(BookingDbContext context)
    {
        this.RuleLevelCascadeMode=CascadeMode.Stop;
        RuleFor(x=>x.PropertyRoomId).NotNull().WithMessage("Property room id is required");
        
        RuleFor(x=>x.PropertyRoomId)
            .Must(x=> context.PropertyRooms.Any(r=>r.Id==x))
            .WithMessage("Property room with that id doesnt exist");
        
        RuleFor(x=>x.StartDate).NotNull().WithMessage("Start date is required");
        
        RuleFor(x=>x.StartDate)
            .Must(x=>x>DateTime.Now)
            .WithMessage("Start date cannot be in the past");
        
        RuleFor(x=>x)
            .Must(x=>x.EndDate>x.StartDate).When(x=>x.EndDate!=null)
            .WithMessage("End date cannot be before start date");
        
        
        
        RuleFor(x=>x.Price).NotEmpty().WithMessage("Price is required");

        RuleFor(x => x)
            .Must(x =>
            {
                // Match the same Kind=Utc conversion EfAttachPriceCommand uses when
                // it actually stores each day's price, so the comparison lines up
                // exactly with what's in the database (and Npgsql doesn't reject an
                // Unspecified-kind DateTime when this gets translated to SQL).
                var startDate = x.StartDate.Date.ToUniversalTime();
                var endDate = (x.EndDate ?? x.StartDate).Date.ToUniversalTime();

                return !context.PropertyRoomPrices.Any(p =>
                    p.PropertyRoomId == x.PropertyRoomId &&
                    p.Date >= startDate &&
                    p.Date <= endDate);
            })
            .WithMessage("A price already exists for this property room on one or more of the selected dates");
    }

}