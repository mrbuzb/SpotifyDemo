using Microsoft.AspNetCore.Http;

namespace Spotify.Application.Services.Interfaces;

public interface ICloudService
{
    Task<string> UploadProfileImageAsync(IFormFile file);
    Task<string> UploadTrackAsync(IFormFile file);
}
