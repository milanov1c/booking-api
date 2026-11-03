using Booking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class UserConfiguration : BaseConfiguration<User>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.Property(x=>x.Password).IsRequired();
        builder.Property(x=>x.Email).IsRequired();
        builder.Property(x=>x.FirstName).IsRequired();
        builder.Property(x=>x.LastName).IsRequired();
        
        builder.HasIndex(x=>x.Email).IsUnique();

        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.HasOne(x => x.Role).WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId);
    }
}