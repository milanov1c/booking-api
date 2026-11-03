using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetRolesQuery : EfUseCase, IGetRolesQuery
{
    public EfGetRolesQuery(BookingDbContext context) : base(context)
    {
    }

    public List<RoleListItemDTO> Execute(EmptyQueryParams param)
    {
        return ctx.Roles
            .OrderBy(r => r.Name)
            .Select(r => new RoleListItemDTO
            {
                Id = r.Id,
                Name = r.Name,
            })
            .ToList();
    }

    public string Name => "Get Roles";
    public string Id => "ef-get-roles";
}
