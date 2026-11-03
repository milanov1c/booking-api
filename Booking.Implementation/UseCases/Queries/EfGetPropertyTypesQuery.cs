using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetPropertyTypesQuery : EfUseCase, IGetPropertyTypesQuery
{
    public EfGetPropertyTypesQuery(BookingDbContext context) : base(context)
    {
    }

    public List<PropertyTypeDTO> Execute(EmptyQueryParams param)
    {
        return ctx.PropertyTypes
            .OrderBy(pt => pt.Name)
            .Select(pt => new PropertyTypeDTO
            {
                Id = pt.Id,
                Name = pt.Name,
            })
            .ToList();
    }

    public string Name => "Get Property Types";
    public string Id => "ef-get-property-types";
}
