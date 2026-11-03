using Booking.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class ReservationConfiguration : BaseConfiguration<Reservation>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Reservation> builder)
    {
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x=>x.TotalPrice).IsRequired();
        builder.Property(x=>x.Status).IsRequired();

        builder.HasIndex(x => new { x.PropertyRoomId, x.StartDate, x.EndDate });

        builder.HasOne(x=>x.User)
            .WithMany(x=>x.Reservations)
            .HasForeignKey(x=>x.UserId);
        
        builder.HasOne(x=>x.PropertyRoom)
            .WithMany(x=>x.Reservations)
            .HasForeignKey(x=>x.PropertyRoomId);
    }
}