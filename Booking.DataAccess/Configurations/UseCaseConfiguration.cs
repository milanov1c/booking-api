using Booking.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class UseCaseConfiguration : BaseConfiguration<UseCase>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UseCase> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x=>x.UseCaseId).IsRequired();
        
        builder.HasIndex(x=>x.UseCaseId).IsUnique();
    }
}