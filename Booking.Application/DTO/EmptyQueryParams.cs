namespace Booking.Application.DTO;

// Shared, reusable "no input" params type for queries that just return a full
// list with nothing to filter on (Countries, Destinations, PropertyTypes,
// Roles, UseCases, Amenities...). Kept as a dedicated type (instead of e.g.
// "object" or "int") to match the IQuery<TParam, TResponse> pattern used
// everywhere else.
public class EmptyQueryParams
{
}
