namespace Spotify.Application.Dtos;

public class PlaylistCreateDto
{
    public string Name { get; set; } 
    public string Description { get; set; }
    public long UserId { get; set; }
    public List<long> TrackIds { get; set; }
}