using Spotify.Application.Services.Interfaces;

namespace Spotify.Api.Endpoints;

public static class UserLikedTrackEndpoints
{
    public static void MapUserLikedTrackEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/user-liked-tracks")
                       .RequireAuthorization()
                       .WithTags("UserLikedTracks");

        group.MapPost("{trackId:long}/like", async (
            long trackId,
            HttpContext context,
            IUserLikedTrackService service) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (userIdStr == null)
                return Results.Unauthorized();

            await service.LikeTrackAsync(long.Parse(userIdStr), trackId);
            return Results.Ok("Track liked successfully");
        });

        group.MapDelete("{trackId:long}/unlike", async (
            long trackId,
            HttpContext context,
            IUserLikedTrackService service) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (userIdStr == null)
                return Results.Unauthorized();

            await service.UnlikeTrackAsync(long.Parse(userIdStr), trackId);
            return Results.Ok("Track unliked successfully");
        });

        group.MapGet("", async (
            HttpContext context,
            IUserLikedTrackService service) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (userIdStr == null)
                return Results.Unauthorized();

            var result = await service.GetLikedTracksAsync(long.Parse(userIdStr));
            return Results.Ok(result);
        });

        group.MapGet("favorite-genres", async (
            HttpContext context,
            IUserLikedTrackService service,
            int top = 3) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (userIdStr == null)
                return Results.Unauthorized();

            var result = await service.GetFavoriteGenresAsync(long.Parse(userIdStr), top);
            return Results.Ok(result);
        });
    }
}
