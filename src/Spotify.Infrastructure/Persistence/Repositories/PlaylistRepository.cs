using Microsoft.EntityFrameworkCore;
using Spotify.Application.Interfaces;
using Spotify.Core.Errors;
using Spotify.Domain.Entities;

namespace Spotify.Infrastructure.Persistence.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly AppDbContext _context;
    public PlaylistRepository(AppDbContext context) => _context = context;

    public async Task<Playlist> GetByIdAsync(long id)
    {
        var playList = await _context.Playlists.Include(p => p.Tracks).FirstOrDefaultAsync(p => p.Id == id);
        if(playList is  null)
        {
            throw new EntityNotFoundException($"PlayList not found with idm {id}");
        }
        return playList;
    }

    public async Task<ICollection<Playlist>> GetByUserIdAsync(long userId)
        => await _context.Playlists.Where(p => p.UserId == userId).ToListAsync();

    public async Task<long> AddAsync(Playlist playlist)
    {
        await _context.Playlists.AddAsync(playlist);
        await _context.SaveChangesAsync();
        return playlist.Id;
    }

    public async Task UpdateAsync(Playlist playlist)
    {
        _context.Playlists.Update(playlist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _context.Playlists.FindAsync(id);
        if (entity != null)
        {
            _context.Playlists.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}