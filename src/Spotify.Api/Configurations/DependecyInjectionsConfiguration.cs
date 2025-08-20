using CloudinaryDotNet;
using FluentValidation;
using Spotify.Application.Dtos;
using Spotify.Application.Helpers;
using Spotify.Application.Interfaces;
using Spotify.Application.Services.Implementations;
using Spotify.Application.Services.Interfaces;
using Spotify.Application.Validators;
using Spotify.Infrastructure.Persistence.Repositories;

namespace Spotify.Api.Configurations;

public static class DependecyInjectionsConfiguration
{
    public static void ConfigureDependecies(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICloudService, CloudinaryService>();
        services.AddScoped<IPlaylistService, PlaylistService>();
        services.AddScoped<IPlaylistTrackService, PlaylistTrackService>();
        services.AddScoped<ITrackService, TrackService>();
        services.AddScoped<IUserTrackHistoryService, UserTrackHistoryService>();
        services.AddScoped<IUserLikedTrackService, UserLikedTrackService>();
        services.AddScoped<IRecommendationService, RecommendationService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRoleRepository, UserRoleRepository>();
        services.AddScoped<IPlaylistRepository, PlaylistRepository>();
        services.AddScoped<IPlaylistTrackRepository, PlaylistTrackRepository>();
        services.AddScoped<ITrackRepository, TrackRepository>();
        services.AddScoped<IUserLikedTrackRepository, UserLikedTrackRepository>();
        services.AddScoped<IUserTrackHistoryRepository, UserTrackHistoryRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IValidator<UserCreateDto>, UserCreateDtoValidator>();
        services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();
        services.AddSingleton<Cloudinary>();
    }
}
