using Microsoft.EntityFrameworkCore;
using Spotify.Application.Interfaces;
using Spotify.Domain.Entities;

namespace Spotify.Infrastructure.Persistence.Repositories;

public class PlaylistTrackRepository : IPlaylistTrackRepository
{
    private readonly AppDbContext _context;

    public PlaylistTrackRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddTrackToPlaylistAsync(long playlistId, long trackId)
    {
        var entity = new PlaylistTrack
        {
            PlaylistId = playlistId,
            TrackId = trackId
        };

        _context.PlaylistTracks.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveTrackFromPlaylistAsync(long playlistId, long trackId, long userId)
    {
        var entity = await _context.PlaylistTracks
            .Include(x=>x.Playlist).ThenInclude(x=>x.User).FirstOrDefaultAsync(pt => pt.PlaylistId == playlistId && pt.TrackId == trackId);

        if (entity != null && entity.Playlist.UserId == userId)
        {
            _context.PlaylistTracks.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ICollection<Track>> GetTracksByPlaylistIdAsync(long playlistId)
    {
        return await _context.PlaylistTracks
            .Where(pt => pt.PlaylistId == playlistId)
            .Select(pt => pt.Track)
            .ToListAsync();
    }
}