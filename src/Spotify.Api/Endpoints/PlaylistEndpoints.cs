using Spotify.Application.Dtos;
using Spotify.Application.Services.Interfaces;

namespace Spotify.Api.Endpoints;

public static class PlaylistEndpoints
{
    public static void  MapPlaylistEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/playlists")
                       .WithTags("PlaylistManagement")
                       .RequireAuthorization();

        group.MapPost("/", async (HttpContext context, IPlaylistService service, PlaylistCreateDto dto) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();

            var result = await service.AddAsync(dto, long.Parse(userId));
            return Results.Ok(result);
        });

        group.MapPut("/{id:long}", async (HttpContext context, IPlaylistService service,PlaylistUpdateDto dto) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();

            await service.UpdateAsync(long.Parse(userId), dto);
            return Results.Ok();
        });

        group.MapDelete("/{id:long}", async (HttpContext context, IPlaylistService service, long id) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();

            await service.DeleteAsync(id, long.Parse(userId));
            return Results.NoContent();
        });

        group.MapGet("/{id:long}", async (IPlaylistService service, long id) =>
        {
            var playlist = await service.GetByIdAsync(id);
            return Results.Ok(playlist);
        });

        group.MapGet("/", async (HttpContext context, IPlaylistService service) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();

            var playlists = await service.GetByUserIdAsync(long.Parse(userId));
            return Results.Ok(playlists);
        });
    }
}