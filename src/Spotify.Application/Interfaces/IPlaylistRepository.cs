using Spotify.Domain.Entities;

namespace Spotify.Application.Interfaces;

public interface IPlaylistRepository
{
    Task<Playlist> GetByIdAsync(long id);
    Task<ICollection<Playlist>> GetByUserIdAsync(long userId);
    Task<long> AddAsync(Playlist playlist);
    Task UpdateAsync(Playlist playlist);
    Task DeleteAsync(long id,long userId);
}