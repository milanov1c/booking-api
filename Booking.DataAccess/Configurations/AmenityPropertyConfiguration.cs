using Booking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class AmenityPropertyConfiguration : IEntityTypeConfiguration<AmenityProperty>
{
    public void Configure(EntityTypeBuilder<AmenityProperty> builder)
    {
        builder.HasOne(x => x.Amenity)
            .WithMany(x => x.AmenityProperties)
            .HasForeignKey(x => x.AmenityId);
        
        builder.HasOne(x=>x.Property)
            .WithMany(x=>x.AmenityProperties)
            .HasForeignKey(x=>x.PropertyId);
    }
}