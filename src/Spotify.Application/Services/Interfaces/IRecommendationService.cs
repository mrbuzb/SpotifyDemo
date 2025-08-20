using Spotify.Domain.Entities;

namespace Spotify.Application.Services.Interfaces;

public interface IRecommendationService
{
    Task<List<Track>> GetRecommendedTracksAsync(long userId);
}
