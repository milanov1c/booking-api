using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Seeding.Seeders;

public class AttachmentSeeder : EntitySeeder<Attachment>
{
    public override int Order => SeedOrder.Attachment;

    protected override DbSet<Attachment> GetDbSet(BookingDbContext context) => context.Attachments;

    protected override IEnumerable<Attachment> GenerateEntities(BookingDbContext context, int count)
    {
        var properties = context.Properties.OrderBy(x => x.Id).ToList();

        return properties.Select(property => new Attachment
        {
            PropertyId = property.Id,
            Path = $"uploads/properties/{ToSlug(property.Name)}/main.jpg"
        });
    }

    private static string ToSlug(string value) =>
        value.ToLowerInvariant().Replace(' ', '-');
}
