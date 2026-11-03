using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetPropertyQuery : EfUseCase, IGetPropertyQuery
{
    public EfGetPropertyQuery(BookingDbContext context) : base(context)
    {
    }

    public PropertyDTO Execute(int propertyId)
    {
        return ctx.Properties
            .Where(p => p.Id == propertyId)
            .Select(p => new PropertyDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Address = p.Address,
                PropertyType = p.PropertyType.Name,
                City = p.Destination.City,
                Country = p.Destination.Country.Name,
                Rating = p.Reviews.Any() ? (decimal?)p.Reviews.Average(r => r.Rating) : null,
                Images = p.Attachments.Select(a => a.Path).ToList(),
                Rooms = p.PropertyRooms.Select(r => new PropertyRoomDTO
                {
                    Id = r.Id,
                    PropertyId = r.PropertyId,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Images = r.Attachments.Select(a => a.Path).ToList(),
                }).ToList(),
                Amenities = p.AmenityProperties.Select(x=>x.Amenity.Name).ToList()
            })
            .FirstOrDefault();
    }

    public string Name => "Get Property";
    public string Id => "ef-get-property";
}
