using Spotify.Application.Services.Interfaces;

namespace Spotify.Api.Endpoints;

public static class PlaylistTrackEndpoints
{
    public static void MapPlaylistTrackEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/playlists/{playlistId:long}/tracks")
            .WithTags("PlaylistTrackManagement")
                       .RequireAuthorization();

        group.MapPost("/{trackId:long}", async (IPlaylistTrackService service, long playlistId, long trackId) =>
        {
            await service.AddTrackToPlaylistAsync(playlistId, trackId);
            return Results.Ok();
        });

        group.MapDelete("/{trackId:long}", async (HttpContext context, IPlaylistTrackService service, long playlistId, long trackId) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();

            await service.RemoveTrackFromPlaylistAsync(playlistId, trackId,long.Parse(userId));
            return Results.NoContent();
        });

        group.MapGet("/", async (HttpContext context, IPlaylistTrackService service, long playlistId) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();

            var tracks = await service.GetTracksByPlaylistIdAsync(playlistId);
            return Results.Ok(tracks);
        });
    }
}