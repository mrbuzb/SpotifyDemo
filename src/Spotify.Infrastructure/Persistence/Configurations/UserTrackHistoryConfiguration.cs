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
            .WithMany()
            .HasForeignKey(h => h.UserId);

        builder.HasOne(h => h.Track)
            .WithMany()
            .HasForeignKey(h => h.TrackId);

        builder.Property(h => h.PlayCount)
            .HasDefaultValue(1);
    }
}