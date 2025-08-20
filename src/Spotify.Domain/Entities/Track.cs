namespace Spotify.Domain.Entities;

public class Track
{
    public long Id { get; set; }
    public string Title { get; set; }
    public TimeSpan Duration { get; set; }
    public string AudioUrl { get; set; }
    public string Genre { get; set; }        
    public string ArtistName { get; set; }   
    public DateTime ReleaseDate { get; set; }
    public string? AlbumName { get; set; }

    public long UploadedById { get; set; }
    public User UploadedBy { get; set; }

    public ICollection<UserTrackHistory> PlayHistories { get; set; }
    public ICollection<PlaylistTrack> Playlists { get; set; }
    public ICollection<UserLikedTrack> LikedByUsers {  get; set; }
}
