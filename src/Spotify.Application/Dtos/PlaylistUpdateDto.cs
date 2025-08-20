namespace Spotify.Application.Dtos;

public class PlaylistUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<long> TrackIds { get; set; }
}