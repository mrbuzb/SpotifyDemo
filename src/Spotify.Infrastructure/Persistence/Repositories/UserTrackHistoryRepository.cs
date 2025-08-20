using Microsoft.EntityFrameworkCore;
using Spotify.Application.Interfaces;
using Spotify.Domain.Entities;

namespace Spotify.Infrastructure.Persistence.Repositories;

public class UserTrackHistoryRepository(AppDbContext _context) : IUserTrackHistoryRepository
{
    public async Task AddPlayAsync(long userId, long trackId)
    {
        var history = await _context.UserTrackHistorys
            .FirstOrDefaultAsync(h => h.UserId == userId && h.TrackId == trackId);

        if (history is null)
        {
            history = new UserTrackHistory
            {
                UserId = userId,
                TrackId = trackId,
                PlayedAt = DateTime.UtcNow,
                PlayCount = 1
            };
            _context.UserTrackHistorys.Add(history);
        }
        else
        {
            history.PlayedAt = DateTime.UtcNow;
            history.PlayCount++;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<UserTrackHistory>> GetHistoryByUserAsync(long userId)
    {
        return await _context.UserTrackHistorys
            .Include(h => h.Track)
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.PlayedAt)
            .ToListAsync();
    }

    public async Task<ICollection<Track>> GetTopPlayedTracksAsync(long userId, int count = 10)
    {
        return await _context.UserTrackHistorys
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.PlayCount)
            .Select(h => h.Track)
            .Take(count)
            .ToListAsync();
    }

    public async Task<ICollection<string>> GetMostListenedGenresAsync(long userId, int top = 3)
    {
        return await _context.UserTrackHistorys
            .Where(h => h.UserId == userId)
            .Include(h => h.Track)
            .GroupBy(h => h.Track.Genre)
            .OrderByDescending(g => g.Sum(h => h.PlayCount))
            .Select(g => g.Key)
            .Take(top)
            .ToListAsync();
    }
}