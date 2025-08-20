namespace Spotify.Application.Dtos;

public class TrackUpdateDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string ArtistName { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? AlbumName { get; set; }
}