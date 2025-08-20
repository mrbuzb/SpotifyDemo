using Spotify.Application.Dtos;

namespace Spotify.Application.Services.Interfaces;

public interface IUserTrackHistoryService
{
    Task AddPlayAsync(long userId, long trackId);
    Task<ICollection<TrackDto>> GetHistoryByUserAsync(long userId);
    Task<ICollection<string>> GetMostListenedGenresAsync(long userId, int top = 3);
    Task<ICollection<TrackDto>> GetTopPlayedTracksAsync(long userId, int count = 10);
}