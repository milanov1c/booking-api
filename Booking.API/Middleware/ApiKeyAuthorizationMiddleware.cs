using Booking.Application;
using Booking.Application.Commands;
using Booking.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Booking.API.Middleware;

public class ApiKeyAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, BookingDbContext db)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint == null)
        {
            await _next(context);
            return;
        }


        // proverava da li endpoint ima [Authorize]
        var authorize =
            endpoint.Metadata.GetMetadata<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>();

        if (authorize == null)
        {
            await _next(context);
            return;
        }


        // korisnik mora biti ulogovan
        if (!context.User.Identity?.IsAuthenticated ?? true)
        {
            context.Response.StatusCode = 401;
            return;
        }


        // nalazimo command iz action parametara
        var actionDescriptor =
            endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();

        if (actionDescriptor == null)
        {
            await _next(context);
            return;
        }


        var commandType = actionDescriptor.Parameters
            .Select(x => x.ParameterType)
            .FirstOrDefault(x =>
                typeof(IUseCase).IsAssignableFrom(x));


        if (commandType == null)
        {
            await _next(context);
            return;
        }


        var command =
            context.RequestServices.GetService(commandType)
            as IUseCase;


        if (command == null)
        {
            context.Response.StatusCode = 500;
            return;
        }


        // Ako niko (Admin) nije eksplicitno registrovao ovaj use case preko
        // POST /api/usecase, ne enforce-ujemo nista ovde - ostaje samo
        // [Authorize(Roles=...)] provera koja se vec desila. Ovo sprecava da
        // svi postojeci endpoint-i prestanu da rade cim se ovaj middleware
        // ukljuci, dok admin postepeno ne prikaci role<->use case parove.
        var isRegistered = db.UseCases.Any(u => u.UseCaseId == command.Id);

        if (!isRegistered)
        {
            await _next(context);
            return;
        }


        // uzimamo usecase-e iz JWT-a
        var claim = context.User.Claims
            .FirstOrDefault(x => x.Type == "UseCaseIds");


        if (claim == null)
        {
            context.Response.StatusCode = 403;
            return;
        }


        var userUseCases = claim.Value
            .Split(',', StringSplitOptions.RemoveEmptyEntries);


        // provera user permissiona
        if (!userUseCases.Contains(command.Id))
        {
            context.Response.StatusCode = 403;
            return;
        }


        await _next(context);
    }
}