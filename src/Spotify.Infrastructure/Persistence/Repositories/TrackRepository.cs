using Microsoft.EntityFrameworkCore;
using Spotify.Application.Interfaces;
using Spotify.Core.Errors;
using Spotify.Domain.Entities;

namespace Spotify.Infrastructure.Persistence.Repositories;

public class TrackRepository(AppDbContext _context) : ITrackRepository
{
    public async Task<Track> GetByIdAsync(long id)
    {
        var track = await _context.Tracks.Include(t => t.UploadedBy).FirstOrDefaultAsync(t => t.Id == id);
        if (track == null)
        {
            throw new EntityNotFoundException($"Track not found with id {id}");
        }
        return track;
    }
    public async Task<ICollection<Track>> GetTracksByGenresAsync(IEnumerable<string> genres)
    {
        return await _context.Tracks
            .Where(t => genres.Contains(t.Genre))
            .ToListAsync();
    }
    public async Task<ICollection<Track>> GetAllAsync()
        => await _context.Tracks.ToListAsync();

    public async Task<ICollection<Track>> GetByUserIdAsync(long userId)
        => await _context.Tracks.Where(t => t.UploadedById == userId).ToListAsync();

    public async Task<ICollection<Track>> GetByGenreAsync(string genre)
        => await _context.Tracks.Where(t => t.Genre == genre).ToListAsync();

    public async Task<long> AddAsync(Track track)
    {
        await _context.Tracks.AddAsync(track);
        await _context.SaveChangesAsync();
        return track.Id;
    }

    public async Task UpdateAsync(Track track)
    {
        _context.Tracks.Update(track);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _context.Tracks.FindAsync(id);
        if (entity != null)
        {
            _context.Tracks.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}