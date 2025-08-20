using Microsoft.AspNetCore.Http;
using Spotify.Application.Dtos;

namespace Spotify.Application.Services.Interfaces;

public interface ITrackService
{
    Task<TrackDto> GetByIdAsync(long id);
    Task<ICollection<TrackDto>> GetAllAsync();
    Task<ICollection<TrackDto>> GetByUserIdAsync(long userId);
    Task<ICollection<TrackDto>> GetByGenreAsync(string genre);
    Task<ICollection<TrackDto>> GetTracksByGenresAsync(IEnumerable<string> genres);
    Task<long> AddAsync(TrackCreateDto dto,long userId,IFormFile file);
    Task UpdateAsync(TrackUpdateDto dto, long userId);
    Task DeleteAsync(long id,long userId);
}