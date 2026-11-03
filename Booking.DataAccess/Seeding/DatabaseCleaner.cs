using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding;

public static class DatabaseCleaner
{
    public static void ClearAll(BookingDbContext context)
    {
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""AuditLogs""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""AuthTokens"" WHERE ""BaseTokenId"" IS NOT NULL");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""AuthTokens""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""Reservations""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""Attachments""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""PropertyRoomPrices""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""PropertyRooms""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""Properties""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""Destinations""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""PropertyTypes""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""Countries""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""RoleUseCases""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""Users""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""UseCases""");
        context.Database.ExecuteSqlRaw(@"DELETE FROM ""Roles""");
    }
}
