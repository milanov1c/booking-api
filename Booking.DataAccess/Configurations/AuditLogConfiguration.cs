using Booking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class AuditLogConfiguration : BaseConfiguration<AuditLog>
{
    protected override void ConfigureEntity(EntityTypeBuilder<AuditLog> builder)
    {
        builder.Property(x => x.CommandId).HasMaxLength(100).IsRequired();
        builder.Property(x => x.CommandName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.UserEmail).HasMaxLength(200);
        builder.Property(x => x.ExecutedAt).IsRequired();
        builder.Property(x => x.Success).IsRequired();
        builder.Property(x => x.ErrorMessage).HasColumnType("TEXT");

        builder.HasIndex(x => x.ExecutedAt);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CommandId);
    }
}
