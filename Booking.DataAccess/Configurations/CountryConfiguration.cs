using Booking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;


public class CountryConfiguration : BaseConfiguration<Country>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Country> builder)
    {
        builder.Property(x=>x.Name).HasMaxLength(30).IsRequired();
        
        builder.HasIndex(x=>x.Name).IsUnique();
    }
}