using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetPropertyRoomQuery : EfUseCase, IGetPropertyRoomQuery
{
    public EfGetPropertyRoomQuery(BookingDbContext context) : base(context)
    {
    }

    public PropertyRoomDTO Execute(int propertyRoomId)
    {
        return ctx.PropertyRooms
            .Where(r => r.Id == propertyRoomId)
            .Select(r => new PropertyRoomDTO
            {
                Id = r.Id,
                PropertyId = r.PropertyId,
                Name = r.Name,
                Capacity = r.Capacity,
                Images = r.Attachments.Select(a => a.Path).ToList(),
            })
            .FirstOrDefault();
    }

    public string Name => "Get Property Room";
    public string Id => "ef-get-property-room";
}
