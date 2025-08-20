using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Entities;

namespace Spotify.Infrastructure.Persistence.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Genre)
            .HasMaxLength(100);

        builder.Property(t => t.ArtistName)
            .HasMaxLength(150);

        builder.HasOne(t => t.UploadedBy)
            .WithMany(u => u.Tracks)
            .HasForeignKey(t => t.UploadedById);
    }
}