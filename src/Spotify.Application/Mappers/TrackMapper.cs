using Spotify.Application.Dtos;
using Spotify.Domain.Entities;

namespace Spotify.Application.Mappers;

public static class TrackMapper
{
    public static TrackDto ToDto(this Track track)
    {
        return new TrackDto
        {
            Id = track.Id,
            Title = track.Title,
            DurationSeconds = track.Duration.TotalSeconds,
            AudioUrl = track.AudioUrl,
            Genre = track.Genre,
            ArtistName = track.ArtistName,
            ReleaseDate = track.ReleaseDate,
            AlbumName = track.AlbumName,
            UploadedById = track.UploadedById
        };
    }

    public static IEnumerable<TrackDto> ToDtoList(this IEnumerable<Track> tracks)
    {
        return tracks.Select(t => t.ToDto());
    }
}