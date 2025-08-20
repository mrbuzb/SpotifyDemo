using Spotify.Domain.Entities;

namespace Spotify.Application.Interfaces;

public interface IUserTrackHistoryRepository
{
    Task AddPlayAsync(long userId, long trackId);
    Task<ICollection<UserTrackHistory>> GetHistoryByUserAsync(long userId);
    Task<ICollection<string>> GetMostListenedGenresAsync(long userId, int top = 3);
    Task<ICollection<Track>> GetTopPlayedTracksAsync(long userId, int count = 10);
}
