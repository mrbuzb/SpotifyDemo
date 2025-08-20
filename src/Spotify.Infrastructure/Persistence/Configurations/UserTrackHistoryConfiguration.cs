using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Entities;

namespace Spotify.Infrastructure.Persistence.Configurations;

public class UserTrackHistoryConfiguration : IEntityTypeConfiguration<UserTrackHistory>
{
    public void Configure(EntityTypeBuilder<UserTrackHistory> builder)
    {
        builder.HasKey(h => h.Id);

        builder.HasOne(h => h.User)
               .WithMany(u => u.TrackHistories)
               .HasForeignKey(h => h.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(h => h.Track)
               .WithMany(t => t.PlayHistories)
               .HasForeignKey(h => h.TrackId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(h => h.PlayCount)
               .HasDefaultValue(1);
    }
}