namespace Spotify.Application.Dtos;

public class TrackDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public double DurationSeconds { get; set; }
    public string AudioUrl { get; set; }
    public string Genre { get; set; }
    public string ArtistName { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? AlbumName { get; set; }
    public long UploadedById { get; set; }
}