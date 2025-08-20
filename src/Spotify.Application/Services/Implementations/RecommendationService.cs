using Spotify.Application.Interfaces;
using Spotify.Application.Services.Interfaces;
using Spotify.Domain.Entities;

namespace Spotify.Application.Services.Implementations;

public class RecommendationService : IRecommendationService
{
    private readonly IUserTrackHistoryRepository _trackHistoryRepo;
    private readonly ITrackRepository _trackRepo;
    private readonly IUserLikedTrackRepository _likedRepo;

    public RecommendationService(
        IUserTrackHistoryRepository trackHistoryRepo,
        ITrackRepository trackRepo,
        IUserLikedTrackRepository likedRepo)
    {
        _trackHistoryRepo = trackHistoryRepo;
        _trackRepo = trackRepo;
        _likedRepo = likedRepo;
    }

    public async Task<List<Track>> GetRecommendedTracksAsync(long userId)
    {
        var history = await _trackHistoryRepo.GetHistoryByUserAsync(userId);

        if (!history.Any())
            return new List<Track>();

        var genreScores = new Dictionary<string, double>();

        foreach (var h in history)
        {
            if (string.IsNullOrEmpty(h.Track.Genre))
                continue;

            double score = 1;

            score += h.PlayCount * 0.5;

            if (h.IsLiked)
                score += 5;

            if (genreScores.ContainsKey(h.Track.Genre))
                genreScores[h.Track.Genre] += score;
            else
                genreScores[h.Track.Genre] = score;
        }

        var topGenres = genreScores
            .OrderByDescending(x => x.Value)
            .Take(2)
            .Select(x => x.Key)
            .ToList();

        var recommended = await _trackRepo.GetTracksByGenresAsync(topGenres);

        var listenedIds = history.Select(h => h.TrackId).ToHashSet();
        var final = recommended.Where(t => !listenedIds.Contains(t.Id)).ToList();

        return final;
    }
}