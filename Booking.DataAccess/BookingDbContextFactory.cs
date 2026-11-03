using Microsoft.EntityFrameworkCore.Design;

namespace Booking.DataAccess;

/// <summary>
/// Used only by EF Core design-time tools (dotnet ef migrations add / database update).
/// BookingDbContext's constructor takes a raw connection string instead of
/// DbContextOptions&lt;T&gt;, so the tools can't construct it on their own without this
/// factory. This never runs at application runtime - Program.cs wires up the real
/// connection string via SetupApplication/AppSettings instead.
///
/// Keep this connection string in sync with appsettings.Development.json's
/// "ConnString" value (or point it at whatever local Postgres instance you use
/// for running migrations).
/// </summary>
public class BookingDbContextFactory : IDesignTimeDbContextFactory<BookingDbContext>
{
    public BookingDbContext CreateDbContext(string[] args)
    {
        const string connectionString =
            "Host=localhost;Port=5432;Database=Booking;Username=postgres;Password=2707";

        return new BookingDbContext(connectionString);
    }
}
