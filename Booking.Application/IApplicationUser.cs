namespace Booking.Application;

public interface IApplicationUser
{
    public int Id { get; }
    public string Email { get; }
    public string Role { get; }

    /// <summary>
    /// UseCaseId values (UseCase.UseCaseId business key, e.g. "ef-add-property")
    /// this user's role is explicitly allowed to execute, as carried by the
    /// "UseCaseIds" JWT claim. Only enforced by UseCaseHandler/ApiKeyAuthorizationMiddleware
    /// for use cases an admin has actually registered via POST /api/usecase -
    /// everything else remains governed purely by [Authorize(Roles=...)].
    /// </summary>
    public IEnumerable<string> AllowedUseCases { get; }
}