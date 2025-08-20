using Spotify.Application.Dtos;

namespace Spotify.Application.Services.Interfaces;

public interface IPlaylistService
{
    Task<PlaylistDto> GetByIdAsync(long id);
    Task<ICollection<PlaylistDto>> GetByUserIdAsync(long userId);
    Task<long> AddAsync(PlaylistCreateDto playlistDto, long userId);
    Task UpdateAsync(long userId, PlaylistUpdateDto playlistDto);
    Task DeleteAsync(long id, long userId);
}