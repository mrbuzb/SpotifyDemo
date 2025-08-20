using Spotify.Application.Dtos;
using Spotify.Application.Interfaces;
using Spotify.Application.Mappers;
using Spotify.Domain.Entities;

namespace Spotify.Application.Services.Implementations;

public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepository _playlistRepo;
    private readonly ITrackRepository _trackRepo;

    public PlaylistService(IPlaylistRepository playlistRepo, ITrackRepository trackRepo)
    {
        _playlistRepo = playlistRepo;
        _trackRepo = trackRepo;
    }

    public async Task<PlaylistDto> GetByIdAsync(long id)
    {
        var playlist = await _playlistRepo.GetByIdAsync(id);

        return new PlaylistDto
        {
            Id = playlist.Id,
            Name = playlist.Name,
            UserId = playlist.UserId,
            Tracks = playlist.Tracks
                .Select(pt => pt.Track.ToDto())
                .ToList()
        };
    }

    public async Task<ICollection<PlaylistDto>> GetByUserIdAsync(long userId)
    {
        var playlists = await _playlistRepo.GetByUserIdAsync(userId);

        return playlists.Select(p => new PlaylistDto
        {
            Id = p.Id,
            Name = p.Name,
            UserId = p.UserId,
            Tracks = p.Tracks
                .Select(pt => pt.Track.ToDto())
                .ToList()
        }).ToList();
    }

    public async Task<long> CreateAsync(PlaylistCreateDto dto)
    {
        var playlist = new Playlist
        {
            Name = dto.Name,
            UserId = dto.UserId,
            Tracks = new List<PlaylistTrack>()
        };

        foreach (var trackId in dto.TrackIds)
        {
            var track = await _trackRepo.GetByIdAsync(trackId);
            if (track != null)
            {
                playlist.Tracks.Add(new PlaylistTrack
                {
                    TrackId = track.Id,
                    Playlist = playlist
                });
            }
        }

        return await _playlistRepo.AddAsync(playlist);
    }

    public async Task UpdateAsync(PlaylistUpdateDto dto)
    {
        var playlist = await _playlistRepo.GetByIdAsync(dto.Id);

        playlist.Name = dto.Name;

        playlist.Tracks.Clear();
        foreach (var trackId in dto.TrackIds)
        {
            var track = await _trackRepo.GetByIdAsync(trackId);
            if (track != null)
            {
                playlist.Tracks.Add(new PlaylistTrack
                {
                    TrackId = track.Id,
                    PlaylistId = playlist.Id
                });
            }
        }

        await _playlistRepo.UpdateAsync(playlist);
    }

    public async Task DeleteAsync(long id)
    {
        await _playlistRepo.DeleteAsync(id);
    }
}
