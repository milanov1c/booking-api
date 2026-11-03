using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetDestinationsQuery : EfUseCase, IGetDestinationsQuery
{
    public EfGetDestinationsQuery(BookingDbContext context) : base(context)
    {
    }

    public List<DestinationDTO> Execute(EmptyQueryParams param)
    {
        return ctx.Destinations
            .OrderBy(d => d.City)
            .Select(d => new DestinationDTO
            {
                Id = d.Id,
                City = d.City,
                CountryId = d.CountryId,
                CountryName = d.Country.Name,
            })
            .ToList();
    }

    public string Name => "Get Destinations";
    public string Id => "ef-get-destinations";
}
