using Booking.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class PropertyRoomConfiguration : BaseConfiguration<PropertyRoom>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PropertyRoom> builder)
    {
        builder.Property(x=>x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x=>x.Capacity).IsRequired();
        
        builder.HasIndex(x=>x.Name).IsUnique();
        
        builder.HasOne(x=>x.Property)
            .WithMany(x=>x.PropertyRooms)
            .HasForeignKey(x=>x.PropertyId);
    }
}