using Microsoft.AspNetCore.Http;
using Spotify.Application.Dtos;
using Spotify.Application.Interfaces;
using Spotify.Application.Mappers;
using Spotify.Application.Services.Interfaces;
using Spotify.Application.Settings;
using Spotify.Domain.Entities;

namespace Spotify.Application.Services.Implementations;

public class TrackService : ITrackService
{
    private readonly ITrackRepository _trackRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICloudService _fileService;

    public TrackService(
        ITrackRepository trackRepository,
        IUserRepository userRepository,
        ICloudService fileService)
    {
        _trackRepository = trackRepository;
        _userRepository = userRepository;
        _fileService = fileService;
    }

    public async Task<TrackDto> GetByIdAsync(long id)
    {
        var track = await _trackRepository.GetByIdAsync(id);
        if (track == null)
        {
            throw new Exception("Track not found");
        }
        return track.ToDto();
    }

    public async Task<ICollection<TrackDto>> GetAllAsync()
    {
        var tracks = await _trackRepository.GetAllAsync();
        return tracks.ToDtoList().ToList();
    }

    public async Task<ICollection<TrackDto>> GetByUserIdAsync(long userId)
    {
        var tracks = await _trackRepository.GetByUserIdAsync(userId);
        return tracks.ToDtoList().ToList();
    }

    public async Task<ICollection<TrackDto>> GetByGenreAsync(string genre)
    {
        var tracks = await _trackRepository.GetByGenreAsync(genre);
        return tracks.ToDtoList().ToList();
    }

    public async Task<ICollection<TrackDto>> GetTracksByGenresAsync(IEnumerable<string> genres)
    {
        var tracks = await _trackRepository.GetTracksByGenresAsync(genres);
        return tracks.ToDtoList().ToList();
    }

    public async Task<long> AddAsync(TrackCreateDto dto, long userId, IFormFile file)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        //var duration = GetAudioDuration(file);
        var filePath = await _fileService.UploadTrackAsync(file);

        var track = new Track
        {
            Title = dto.Title,
            Genre = dto.Genre,
            AudioUrl = filePath,
            UploadedById = userId,
            ArtistName = dto.ArtistName,
            AlbumName = dto.AlbumName,
            ReleaseDate = dto.ReleaseDate,
            //Duration = duration
        };

        await _trackRepository.AddAsync(track);
        return track.Id;
    }

    public TimeSpan GetAudioDuration(IFormFile file)
    {
        using var inputStream = file.OpenReadStream();
        using var memoryStream = new MemoryStream();
        inputStream.CopyTo(memoryStream);

        memoryStream.Position = 0;

        var tagFile = TagLib.File.Create(new StreamFileAbstraction(file.FileName, memoryStream, memoryStream));
        return tagFile.Properties.Duration;
    }

    public async Task UpdateAsync(TrackUpdateDto dto, long userId)
    {
        var track = await _trackRepository.GetByIdAsync(dto.Id);
        if (track == null)
        {
            throw new Exception("Track not found");
        }
        if (track.UploadedById != userId)
        {
            throw new UnauthorizedAccessException("You cannot update this track");
        }

        track.Title = dto.Title ?? track.Title;
        track.Genre = dto.Genre ?? track.Genre;
        track.AlbumName = dto.AlbumName ?? track.AlbumName;
        track.ArtistName = dto.ArtistName ?? track.ArtistName;
        track.ReleaseDate = dto.ReleaseDate;

        await _trackRepository.UpdateAsync(track);
    }

    public async Task DeleteAsync(long id, long userId)
    {
        var track = await _trackRepository.GetByIdAsync(id);
        if (track == null) throw new Exception("Track not found");
        if (track.UploadedById != userId)
        {
            throw new UnauthorizedAccessException("You cannot delete this track");
        }
        await _trackRepository.DeleteAsync(id);
    }
}
