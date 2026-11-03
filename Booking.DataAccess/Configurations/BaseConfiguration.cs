using Booking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        
        builder.Property(x=>x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        ConfigureEntity(builder);
        
    }
    
    protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}