using Booking.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class PropertyRoomPriceConfiguration : BaseConfiguration<PropertyRoomPrice>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PropertyRoomPrice> builder)
    {
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        
        builder.HasOne(x=>x.PropertyRoom)
            .WithMany(x=>x.PropertyRoomPrices)
            .HasForeignKey(x=>x.PropertyRoomId);
    }
}