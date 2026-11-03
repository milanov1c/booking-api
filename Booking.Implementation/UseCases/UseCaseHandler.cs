using System.Diagnostics;
using Application.Exceptions;
using Booking.Application;
using Booking.DataAccess;
using Booking.Domain;

namespace Booking.Implementation.UseCases;

public class UseCaseHandler
{
    private readonly IApplicationUser _user;
    private readonly BookingDbContext _context;

    public UseCaseHandler(IApplicationUser user, BookingDbContext context)
    {
        _user = user;
        _context = context;
    }

    /// <summary>
    /// Enforces role-based use-case permissions, but only for use cases an
    /// admin has explicitly registered via POST /api/usecase (and attached
    /// to roles via POST /api/usecase/attach). Anything not registered is
    /// left entirely to [Authorize(Roles=...)] on the controller action -
    /// this keeps the ~70 existing commands/queries working unchanged while
    /// letting an admin opt individual ones into finer-grained control.
    /// </summary>
    private void HandleAuthorization(IUseCase useCase)
    {
        var isRegistered = _context.UseCases.Any(u => u.UseCaseId == useCase.Id);

        if (!isRegistered)
        {
            return;
        }

        if (!_user.AllowedUseCases.Contains(useCase.Id))
        {
            throw new UnauthorizedUseCaseException(_user.Email, useCase.Name);
        }
    }

    public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
    {
        HandleAuthorization(command);

        Stopwatch stopwatch = Stopwatch.StartNew();
        bool success = true;
        string errorMessage = null;

        try
        {
            command.Execute(request);
        }
        catch (Exception ex)
        {
            success = false;
            errorMessage = ex.Message;
            throw;
        }
        finally
        {
            stopwatch.Stop();

            _context.AuditLogs.Add(new AuditLog
            {
                UserId = _user.Id > 0 ? _user.Id : null,
                UserEmail = _user.Email,
                CommandId = command.Id,
                CommandName = command.Name,
                ExecutedAt = DateTime.UtcNow,
                Success = success,
                ErrorMessage = errorMessage,
            });

            _context.SaveChanges();

            Console.WriteLine($"{_user.Email} has executed use case: {command.Name}" + stopwatch.ElapsedMilliseconds + " ms. Success: " + success);
        }
    }

    public TResult ExecuteQuery<TParam,TResult>(IQuery<TParam, TResult> query, TParam request)
        where TResult : class
    {
        HandleAuthorization(query);
        Stopwatch stopwatch = Stopwatch.StartNew();
            
        stopwatch.Start();
            
        var result = query.Execute(request);

        stopwatch.Stop();

        Console.WriteLine($"{_user.Email} has executed use case: {query.Name}" + stopwatch.ElapsedMilliseconds + " ms.");

        return result;
    }
}
