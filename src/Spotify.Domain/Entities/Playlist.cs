namespace Spotify.Domain.Entities;

public class Playlist
{
    public long Id { get; set; }
    public string Name { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public ICollection<PlaylistTrack> Tracks { get; set; }
}
