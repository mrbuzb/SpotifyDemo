using Spotify.Application.Dtos;
using Spotify.Application.Interfaces;
using Spotify.Application.Mappers;
using Spotify.Application.Services.Interfaces;

namespace Spotify.Application.Services.Implementations;

public class UserLikedTrackService : IUserLikedTrackService
{
    private readonly IUserLikedTrackRepository _likedRepo;

    public UserLikedTrackService(IUserLikedTrackRepository likedRepo)
    {
        _likedRepo = likedRepo;
    }

    public async Task LikeTrackAsync(long userId, long trackId)
    {
        await _likedRepo.LikeTrackAsync(userId, trackId);
    }

    public async Task UnlikeTrackAsync(long userId, long trackId)
    {
        await _likedRepo.UnlikeTrackAsync(userId, trackId);
    }

    public async Task<IEnumerable<TrackDto>> GetLikedTracksAsync(long userId)
    {
        var tracks = await _likedRepo.GetLikedTracksAsync(userId);

        return tracks.ToDtoList();
    }

    public async Task<ICollection<string>> GetFavoriteGenresAsync(long userId, int top = 3)
    {
        return await _likedRepo.GetFavoriteGenresAsync(userId, top);
    }
}