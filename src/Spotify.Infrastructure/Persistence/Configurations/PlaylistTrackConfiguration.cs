using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotify.Domain.Entities;

namespace Spotify.Infrastructure.Persistence.Configurations;

public class PlaylistTrackConfiguration : IEntityTypeConfiguration<PlaylistTrack>
{
    public void Configure(EntityTypeBuilder<PlaylistTrack> builder)
    {
        builder.HasKey(pt => new { pt.PlaylistId, pt.TrackId });

        builder.HasOne(pt => pt.Playlist)
            .WithMany(p => p.Tracks)
            .HasForeignKey(pt => pt.PlaylistId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pt => pt.Track)
            .WithMany(t => t.Playlists)
            .HasForeignKey(pt => pt.TrackId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
