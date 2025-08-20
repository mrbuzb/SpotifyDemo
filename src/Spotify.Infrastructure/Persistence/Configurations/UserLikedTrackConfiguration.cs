using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Entities;

namespace Spotify.Infrastructure.Persistence.Configurations;

public class UserLikedTrackConfiguration : IEntityTypeConfiguration<UserLikedTrack>
{
    public void Configure(EntityTypeBuilder<UserLikedTrack> builder)
    {
        builder.ToTable("UserLikedTracks");

        builder.HasKey(ult => new { ult.UserId, ult.TrackId });

        builder.Property(ult => ult.LikedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(ult => ult.User)
               .WithMany(u => u.LikedTracks)
               .HasForeignKey(ult => ult.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ult => ult.Track)
               .WithMany(t => t.LikedByUsers)
               .HasForeignKey(ult => ult.TrackId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(ult => ult.LikedAt)
               .HasDefaultValueSql("GETUTCDATE()");
    }
}