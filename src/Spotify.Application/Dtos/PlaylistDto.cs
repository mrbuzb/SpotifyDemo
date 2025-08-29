namespace Spotify.Application.Dtos;

public class PlaylistDto
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public long UserId { get; set; }
    public List<TrackDto> Tracks { get; set; }
}