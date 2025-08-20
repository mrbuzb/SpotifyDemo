using Spotify.Application.Dtos;

namespace Spotify.Application.Services.Interfaces;

public interface IPlaylistTrackService
{
    Task AddTrackToPlaylistAsync(long playlistId, long trackId);
    Task RemoveTrackFromPlaylistAsync(long playlistId, long trackId, long userId);
    Task<IEnumerable<TrackDto>> GetTracksByPlaylistIdAsync(long playlistId);
}