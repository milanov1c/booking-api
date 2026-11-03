using Booking.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;


public class DestinationConfiguration : BaseConfiguration<Destination>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Destination> builder)
    {
        builder.Property(x=>x.City).HasMaxLength(30).IsRequired();
        
        builder.HasIndex(x=>x.City).IsUnique();
        
        builder.HasOne(x=>x.Country)
            .WithMany(x=>x.Destinations)
            .HasForeignKey(x=>x.CountryId);
    }
}