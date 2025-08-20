using Spotify.Domain.Entities;

namespace Spotify.Application.Interfaces;

public interface ITrackRepository
{
    Task<Track> GetByIdAsync(long id);
    Task<ICollection<Track>> GetAllAsync();
    Task<ICollection<Track>> GetByUserIdAsync(long userId);
    Task<ICollection<Track>> GetByGenreAsync(string genre);
    Task<long> AddAsync(Track track);
    Task UpdateAsync(Track track);
    Task DeleteAsync(long id);
    Task<ICollection<Track>> GetTracksByGenresAsync(IEnumerable<string> genres);
}