using Booking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Configurations;

public class AuthTokenConfiguration : BaseConfiguration<AuthToken>
{
    protected override void ConfigureEntity(EntityTypeBuilder<AuthToken> builder)
    {
        builder.Property(x => x.TokenId).HasMaxLength(100).IsRequired();
        builder.HasIndex(x => x.TokenId).IsUnique()
            .IncludeProperties(x => x.InvalidatedAt);

        builder.HasOne(x => x.User)
            .WithMany(x=>x.AuthTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.JwtToken)
            .WithOne(x => x.RefreshToken)
            .HasForeignKey<AuthToken>(x => x.BaseTokenId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}