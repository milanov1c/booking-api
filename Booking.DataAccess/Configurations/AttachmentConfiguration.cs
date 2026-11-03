using Booking.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class AttachmentConfiguration : BaseConfiguration<Attachment>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasOne(x=>x.Property)
            .WithMany(x=>x.Attachments)
            .HasForeignKey(x=>x.PropertyId);

        builder.HasOne(x => x.PropertyRoom)
            .WithMany(x => x.Attachments)
            .HasForeignKey(x => x.PropertyRoomId);
    }
}