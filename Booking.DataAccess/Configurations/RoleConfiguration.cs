using Booking.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class RoleConfiguration : BaseConfiguration<Role>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
    {
        builder.HasIndex(x=>x.Name).IsUnique();
        builder.Property(x=>x.Name).IsRequired();
    }
}