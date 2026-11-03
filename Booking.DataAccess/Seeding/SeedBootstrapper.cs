using Microsoft.Extensions.Logging;

namespace Booking.DataAccess.Seeding;

public static class SeedBootstrapper
{
    public static void Run(
        BookingDbContext context,
        DatabaseSeeder seeder,
        SeedSettings seedSettings,
        ILogger logger)
    {
        try
        {
            if (seedSettings.ResetOnStartup)
            {
                logger.LogWarning("Seed reset is enabled. Clearing all tables...");
                seeder.Reseed(context);
                LogTableCounts(context, logger, "Database reseed completed.");
                return;
            }

            logger.LogInformation("Running database seed for empty tables...");
            seeder.Seed(context);
            LogTableCounts(context, logger, "Database seed completed.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database seed failed.");
            throw;
        }
    }

    private static void LogTableCounts(BookingDbContext context, ILogger logger, string message)
    {
        logger.LogInformation(
            "{Message} Rows => Roles: {Roles}, Users: {Users}, Countries: {Countries}, Destinations: {Destinations}, PropertyTypes: {PropertyTypes}, Properties: {Properties}, PropertyRooms: {PropertyRooms}, Reservations: {Reservations}",
            message,
            context.Roles.Count(),
            context.Users.Count(),
            context.Countries.Count(),
            context.Destinations.Count(),
            context.PropertyTypes.Count(),
            context.Properties.Count(),
            context.PropertyRooms.Count(),
            context.Reservations.Count());
    }
}
