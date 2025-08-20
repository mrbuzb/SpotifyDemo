using Spotify.Application.Dtos;

namespace Spotify.Application.Services.Interfaces;

public interface IUserLikedTrackService
{
    Task LikeTrackAsync(long userId, long trackId);
    Task UnlikeTrackAsync(long userId, long trackId);
    Task<IEnumerable<TrackDto>> GetLikedTracksAsync(long userId);
    Task<ICollection<string>> GetFavoriteGenresAsync(long userId, int top = 3);
}