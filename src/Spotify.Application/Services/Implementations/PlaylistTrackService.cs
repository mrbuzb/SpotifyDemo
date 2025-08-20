using Spotify.Application.Dtos;
using Spotify.Application.Interfaces;
using Spotify.Application.Mappers;
using Spotify.Application.Services.Interfaces;

namespace Spotify.Application.Services.Implementations;

public class PlaylistTrackService : IPlaylistTrackService
{
    private readonly IPlaylistTrackRepository _playlistTrackRepository;

    public PlaylistTrackService(IPlaylistTrackRepository playlistTrackRepository)
    {
        _playlistTrackRepository = playlistTrackRepository;
    }

    public async Task AddTrackToPlaylistAsync(long playlistId, long trackId)
    {
        await _playlistTrackRepository.AddTrackToPlaylistAsync(playlistId, trackId);
    }

    public async Task RemoveTrackFromPlaylistAsync(long playlistId, long trackId)
    {
        await _playlistTrackRepository.RemoveTrackFromPlaylistAsync(playlistId, trackId);
    }

    public async Task<IEnumerable<TrackDto>> GetTracksByPlaylistIdAsync(long playlistId)
    {
        var tracks = await _playlistTrackRepository.GetTracksByPlaylistIdAsync(playlistId);

        return tracks.ToDtoList();
    }
}