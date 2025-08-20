using Spotify.Domain.Entities;

namespace Spotify.Application.Interfaces;

public interface IUserLikedTrackRepository
{
    Task LikeTrackAsync(long userId, long trackId);
    Task UnlikeTrackAsync(long userId, long trackId);
    Task<ICollection<Track>> GetLikedTracksAsync(long userId);
    Task<ICollection<string>> GetFavoriteGenresAsync(long userId, int top = 3);
}
