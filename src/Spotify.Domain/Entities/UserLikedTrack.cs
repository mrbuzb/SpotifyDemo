namespace Spotify.Domain.Entities;

public class UserLikedTrack
{
    public long Id { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long TrackId { get; set; }
    public Track Track { get; set; }

    public DateTime LikedAt { get; set; }
}
