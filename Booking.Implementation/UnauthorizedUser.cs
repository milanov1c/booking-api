using Booking.Application;

namespace Booking.Implementation;

public class UnauthorizedUser : IApplicationUser
{
    public int Id => 0;
    public string Email => "test@ict.edu.rs";
    public string Username => "guest";
    public string Role => "";

    // Anonymous/unauthenticated caller: only the use cases backing the
    // genuinely public endpoints (no [Authorize] attribute) are allowed here.
    // If any of these ever gets registered via POST /api/usecase for
    // fine-grained checking, anonymous users will still be able to call it.
    public IEnumerable<string> AllowedUseCases => new[]
    {
        "ef-search-properties",
        "ef-get-property",
        "ef-get-property-room",
        "ef-check-property-room-availability",
        "ef-get-property-reviews",
        "ef-get-countries",
        "ef-get-destinations",
        "ef-get-amenities",
        "ef-get-property-types",
        "ef-get-roles",
        "ef-register-user",
    };
}