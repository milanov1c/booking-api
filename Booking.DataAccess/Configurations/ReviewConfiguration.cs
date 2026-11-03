using Booking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.Property(x=>x.Comment)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x=>x.Rating).IsRequired();


        builder.HasOne(x => x.Property)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.PropertyId);
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.UserId);
    }
}