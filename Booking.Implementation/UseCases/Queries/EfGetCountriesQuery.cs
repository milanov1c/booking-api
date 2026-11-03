using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetCountriesQuery : EfUseCase, IGetCountriesQuery
{
    public EfGetCountriesQuery(BookingDbContext context) : base(context)
    {
    }

    public List<CountryDTO> Execute(EmptyQueryParams param)
    {
        return ctx.Countries
            .OrderBy(c => c.Name)
            .Select(c => new CountryDTO
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToList();
    }

    public string Name => "Get Countries";
    public string Id => "ef-get-countries";
}
