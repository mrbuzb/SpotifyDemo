using Microsoft.EntityFrameworkCore;
using Spotify.Application.Interfaces;
using Spotify.Domain.Entities;

namespace Spotify.Infrastructure.Persistence.Repositories;

public class UserLikedTrackRepository(AppDbContext _context) : IUserLikedTrackRepository
{
    public async Task LikeTrackAsync(long userId, long trackId)
    {
        var alreadyLiked = await _context.UserLikedTracks
            .AnyAsync(lt => lt.UserId == userId && lt.TrackId == trackId);

        if (!alreadyLiked)
        {
            var likedTrack = new UserLikedTrack
            {
                UserId = userId,
                TrackId = trackId,
                LikedAt = DateTime.UtcNow
            };

            await _context.UserLikedTracks.AddAsync(likedTrack);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UnlikeTrackAsync(long userId, long trackId)
    {
        var likedTrack = await _context.UserLikedTracks
            .FirstOrDefaultAsync(lt => lt.UserId == userId && lt.TrackId == trackId);

        if (likedTrack != null)
        {
            _context.UserLikedTracks.Remove(likedTrack);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ICollection<Track>> GetLikedTracksAsync(long userId)
    {
        return await _context.UserLikedTracks
            .Where(lt => lt.UserId == userId)
            .Include(lt => lt.Track)
            .Select(lt => lt.Track)
            .ToListAsync();
    }

    public async Task<ICollection<string>> GetFavoriteGenresAsync(long userId, int top = 3)
    {
        return await _context.UserLikedTracks
            .Where(l => l.UserId == userId)
            .Include(l => l.Track)
            .GroupBy(l => l.Track.Genre)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .Take(top)
            .ToListAsync();
    }
}
