using Spotify.Application.Mappers;
using Spotify.Application.Services.Interfaces;

namespace Spotify.Api.Endpoints;

public static class RecommendationEndpoints
{
    public static void MapRecommendationEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/recommendations")
            .RequireAuthorization()
            .WithTags("RecommendationManagement");

        group.MapGet("", async (HttpContext context, IRecommendationService service) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }

            var tracks = await service.GetRecommendedTracksAsync(long.Parse(userId));
            return Results.Ok(tracks.ToDtoList().ToList());
        });

    }
}
