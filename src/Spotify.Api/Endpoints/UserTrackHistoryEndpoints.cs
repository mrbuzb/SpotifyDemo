using Spotify.Application.Services.Interfaces;

namespace Spotify.Api.Endpoints;

public static class UserTrackHistoryEndpoints
{
    public static void MapUserTrackHistoryEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/user-track-history")
            .RequireAuthorization()
            .WithTags("UserTrackHistoryManagement");

        group.MapPost("add-play/{trackId:long}", async (
            long trackId,
            IUserTrackHistoryService service,
            HttpContext context) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                return Results.Unauthorized();

            await service.AddPlayAsync(long.Parse(userIdStr), trackId);
            return Results.Ok("Track play added.");
        });

        group.MapGet("history", async (
            IUserTrackHistoryService service,
            HttpContext context) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                return Results.Unauthorized();

            var history = await service.GetHistoryByUserAsync(long.Parse(userIdStr));
            return Results.Ok(history);
        });

        group.MapGet("most-listened-genres", async (
            int top,
            IUserTrackHistoryService service,
            HttpContext context) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                return Results.Unauthorized();

            var genres = await service.GetMostListenedGenresAsync(long.Parse(userIdStr), top);
            return Results.Ok(genres);
        });

        group.MapGet("top-played-tracks", async (
            int count,
            IUserTrackHistoryService service,
            HttpContext context) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                return Results.Unauthorized();

            var tracks = await service.GetTopPlayedTracksAsync(long.Parse(userIdStr), count);
            return Results.Ok(tracks);
        });
    }
}
