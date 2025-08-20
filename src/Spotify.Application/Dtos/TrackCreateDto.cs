using Microsoft.AspNetCore.Http;

namespace Spotify.Application.Dtos;

public class TrackCreateDto
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public string ArtistName { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? AlbumName { get; set; }
}