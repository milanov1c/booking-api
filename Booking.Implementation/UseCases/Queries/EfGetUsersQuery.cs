using Booking.Application;
using Booking.Application.DTO;
using Booking.Application.Extensions;
using Booking.Application.Queries;
using Booking.DataAccess;
using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
{
    public EfGetUsersQuery(BookingDbContext context) : base(context)
    {
    }

    public PagedResponse<UserListItemDTO> Execute(GetUsersSearchDTO param)
    {
        IQueryable<User> query = ctx.Users;

        if (!string.IsNullOrWhiteSpace(param.Email))
        {
            query = query.Where(u => EF.Functions.ILike(u.Email, $"%{param.Email}%"));
        }

        if (!string.IsNullOrWhiteSpace(param.RoleName))
        {
            query = query.Where(u => EF.Functions.ILike(u.Role.Name, $"%{param.RoleName}%"));
        }

        if (param.IsActive.HasValue)
        {
            query = query.Where(u => u.IsActive == param.IsActive.Value);
        }

        var projected = query
            .OrderBy(u => u.Email)
            .Select(u => new UserListItemDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                RoleName = u.Role.Name,
                IsActive = u.IsActive,
            });

        return projected.ToPagedResponse(param.Page, param.PageSize);
    }

    public string Name => "Get Users";
    public string Id => "ef-get-users";
}
