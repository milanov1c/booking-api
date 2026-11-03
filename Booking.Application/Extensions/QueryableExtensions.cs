using Booking.Application;

namespace Booking.Application.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Executes the query and wraps the result in a PagedResponse. Works with
    /// any IQueryable&lt;T&gt; (EF Core translates Count/Skip/Take to SQL).
    /// </summary>
    public static PagedResponse<T> ToPagedResponse<T>(this IQueryable<T> query, int page, int pageSize)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 20;
        }

        var totalCount = query.Count();

        var items = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResponse<T>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
        };
    }
}
