using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetAmenitiesQuery : EfUseCase, IGetAmenitiesQuery
{
    public EfGetAmenitiesQuery(BookingDbContext context) : base(context)
    {
    }

    public List<AmenityDTO> Execute(EmptyQueryParams param)
    {
        return ctx.Amenities
            .OrderBy(a => a.Name)
            .Select(a => new AmenityDTO
            {
                Id = a.Id,
                Name = a.Name,
            })
            .ToList();
    }

    public string Name => "Get Amenities";
    public string Id => "ef-get-amenities";
}
