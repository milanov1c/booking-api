namespace Booking.Application.DTO;

// Empty for now - the current user is resolved from the auth token inside the
// query itself. Kept as a dedicated params type (instead of e.g. "int" or
// "object") to match the IQuery<TParam, TResponse> pattern used everywhere
// else, and to leave room for future options (paging the lists, etc.).
public class GetUserProfileDTO
{
}
