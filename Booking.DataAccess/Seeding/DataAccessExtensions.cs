using Booking.DataAccess.Seeding.Seeders;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.DataAccess.Seeding;

public static class DataAccessExtensions
{
    public static IServiceCollection AddDatabaseSeeding(this IServiceCollection services, SeedSettings seedSettings)
    {
        services.AddSingleton(seedSettings);
        services.AddTransient<DatabaseSeeder>();

        services.AddTransient<IDataSeeder, RoleSeeder>();
        services.AddTransient<IDataSeeder, CountrySeeder>();
        services.AddTransient<IDataSeeder, DestinationSeeder>();
        services.AddTransient<IDataSeeder, PropertyTypeSeeder>();
        services.AddTransient<IDataSeeder, PropertySeeder>();
        services.AddTransient<IDataSeeder, PropertyRoomSeeder>();
        services.AddTransient<IDataSeeder, ExtraPropertyRoomSeeder>();
        services.AddTransient<IDataSeeder, PropertyRoomPriceSeeder>();
        services.AddTransient<IDataSeeder, ExtraPropertyRoomPriceSeeder>();
        services.AddTransient<IDataSeeder, AttachmentSeeder>();
        services.AddTransient<IDataSeeder, UserSeeder>();
        services.AddTransient<IDataSeeder, ReservationSeeder>();

        return services;
    }
}
