using Booking.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class PropertyTypeConfiguration : BaseConfiguration<PropertyType>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PropertyType> builder)
    {
        builder.Property(x=>x.Name).HasMaxLength(20).IsRequired();
        
        builder.HasIndex(x=>x.Name).IsUnique();
    }
}