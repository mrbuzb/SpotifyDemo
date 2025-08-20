using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Dtos;
using Spotify.Application.Services.Interfaces;

namespace Spotify.Api.Endpoints;

public static class TrackEndpoints
{
    public static void MapTrackEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/tracks").WithTags("Tracks").RequireAuthorization();

        group.MapGet("/{id:long}", async ([FromServices] ITrackService service, long id) =>
        {
            var track = await service.GetByIdAsync(id);
            return Results.Ok(track);
        });

        group.MapGet("/", async ([FromServices] ITrackService service) =>
        {
            var tracks = await service.GetAllAsync();
            return Results.Ok(tracks);
        });

        group.MapGet("/by-user/{userId:long}", async ([FromServices] ITrackService service, HttpContext context) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (userIdStr == null)
                return Results.Unauthorized();
            var tracks = await service.GetByUserIdAsync(long.Parse(userIdStr));
            return Results.Ok(tracks);
        });

        group.MapGet("/by-genre/{genre}", async ([FromServices] ITrackService service, string genre) =>
        {
            var tracks = await service.GetByGenreAsync(genre);
            return Results.Ok(tracks);
        });

        group.MapPost("/by-genres", async ([FromServices] ITrackService service, [FromBody] IEnumerable<string> genres) =>
        {
            var tracks = await service.GetTracksByGenresAsync(genres);
            return Results.Ok(tracks);
        });

        group.MapPost("/", async ([FromForm] TrackCreateDto dto,IFormFile file,[FromServices] ITrackService service,HttpContext context) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (userIdStr == null)
                return Results.Unauthorized();
            var id = await service.AddAsync(dto,long.Parse(userIdStr),file);
            return Results.Ok(id);
        })
          .DisableAntiforgery();

        group.MapPut("/{userId:long}", async ([FromServices] ITrackService service, [FromBody] TrackUpdateDto dto, HttpContext context) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (userIdStr == null)
                return Results.Unauthorized();
            await service.UpdateAsync(dto, long.Parse(userIdStr));
            return Results.NoContent();
        });

        group.MapDelete("/{id:long}/user/{userId:long}", async ([FromServices] ITrackService service, long id, HttpContext context) =>
        {
            var userIdStr = context.User.FindFirst("UserId")?.Value;
            if (userIdStr == null)
                return Results.Unauthorized();
            await service.DeleteAsync(id, long.Parse(userIdStr));
            return Results.NoContent();
        });
    }
}
