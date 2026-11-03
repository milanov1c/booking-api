using Booking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Booking.DataAccess;

public class BookingDbContext : DbContext
{
    private string _connectionString;

    public BookingDbContext(string connectionString)
    {
        
        _connectionString = connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);
        
        builder.Entity<RoleUseCase>().HasKey(e => new { e.UseCaseId, e.RoleId });
        builder.Entity<AmenityProperty>().HasKey(e => new { e.AmenityId, e.PropertyId });
    }
    
    public DbSet<PropertyType> PropertyTypes { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<PropertyRoom> PropertyRooms { get; set; }
    public DbSet<PropertyRoomPrice> PropertyRoomPrices { get; set; }
    public DbSet<UseCase> UseCases { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleUseCase> RoleUseCases { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<AuthToken> AuthTokens { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<AmenityProperty> AmenityProperties { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
}