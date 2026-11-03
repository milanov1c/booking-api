using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetUseCasesQuery : EfUseCase, IGetUseCasesQuery
{
    public EfGetUseCasesQuery(BookingDbContext context) : base(context)
    {
    }

    public List<UseCaseListItemDTO> Execute(EmptyQueryParams param)
    {
        return ctx.UseCases
            .OrderBy(u => u.Name)
            .Select(u => new UseCaseListItemDTO
            {
                Id = u.Id,
                UseCaseId = u.UseCaseId,
                Name = u.Name,
            })
            .ToList();
    }

    public string Name => "Get Use Cases";
    public string Id => "ef-get-use-cases";
}
