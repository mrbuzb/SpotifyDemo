using Spotify.Application.Dtos;
using Spotify.Application.Interfaces;
using Spotify.Application.Mappers;
using Spotify.Application.Services.Interfaces;

namespace Spotify.Application.Services.Implementations;

public class UserTrackHistoryService : IUserTrackHistoryService
{
    private readonly IUserTrackHistoryRepository _historyRepository;

    public UserTrackHistoryService(IUserTrackHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public async Task AddPlayAsync(long userId, long trackId)
    {
        await _historyRepository.AddPlayAsync(userId, trackId);
    }

    public async Task<ICollection<TrackDto>> GetHistoryByUserAsync(long userId)
    {
        var history = await _historyRepository.GetHistoryByUserAsync(userId);
        return history.Select(h => new TrackDto
        {
            Id = h.Track.Id,
            Title = h.Track.Title,
            DurationSeconds = h.Track.Duration.TotalSeconds,
            AudioUrl = h.Track.AudioUrl,
            Genre = h.Track.Genre,
            ArtistName = h.Track.ArtistName,
            ReleaseDate = h.Track.ReleaseDate,
            AlbumName = h.Track.AlbumName,
            UploadedById = h.Track.UploadedById
        }).ToList();
    }

    public async Task<ICollection<string>> GetMostListenedGenresAsync(long userId, int top = 3)
    {
        return await _historyRepository.GetMostListenedGenresAsync(userId, top);
    }

    public async Task<ICollection<TrackDto>> GetTopPlayedTracksAsync(long userId, int count = 10)
    {
        var tracks = await _historyRepository.GetTopPlayedTracksAsync(userId, count);
        return tracks.ToDtoList().ToList();
    }
}