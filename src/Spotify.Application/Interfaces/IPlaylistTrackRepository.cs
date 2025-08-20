using Spotify.Domain.Entities;

namespace Spotify.Application.Interfaces;

public interface IPlaylistTrackRepository
{
    Task AddTrackToPlaylistAsync(long playlistId, long trackId);
    Task RemoveTrackFromPlaylistAsync(long playlistId, long trackId, long userId);
    Task<ICollection<Track>> GetTracksByPlaylistIdAsync(long playlistId);
}