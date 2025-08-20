namespace Spotify.Domain.Entities;

public class UserTrackHistory
{
    public long Id { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long TrackId { get; set; }
    public Track Track { get; set; }

    public DateTime PlayedAt { get; set; } 
    public int PlayCount { get; set; }     
    public bool IsLiked { get; set; }
}
