using Booking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class PropertyConfiguration : BaseConfiguration<Property>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Property> builder)
    {
        builder.Property(x=>x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x=>x.Address).HasMaxLength(70).IsRequired();
        builder.Property(x => x.Description).HasColumnType("TEXT");
        
        builder.HasIndex(x=>x.Name).IsUnique();

        builder.HasOne(x => x.Destination)
            .WithMany(x => x.Properties)
            .HasForeignKey(x => x.DestinationId);
        
        builder.HasOne(x=>x.PropertyType)
            .WithMany(x=>x.Properties)
            .HasForeignKey(x=>x.PropertyTypeId);

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.OwnedProperties)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}