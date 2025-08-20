using Spotify.Application.Dtos;
using Spotify.Application.Services.Interfaces;

namespace Spotify.Api.Endpoints;

public static class AuthEndpoints
{
    public record SendCodeRequest(string Email);

    public static void MapAuthEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/auth")
            .AllowAnonymous()
            .WithTags("AuthenticationManagement");


        userGroup.MapPost("/send-code",
        async (SendCodeRequest request, IAuthService _service) =>
        {
            if (string.IsNullOrEmpty(request.Email))
                return Results.BadRequest("Email is required");

            await _service.EailCodeSender(request.Email);
            return Results.Ok(new { success = true, data = "Confirmation code sent" });
        })
        .WithName("SendCode");




        userGroup.MapPost("/confirm-code",
        async (ConfirmCodeRequest request, IAuthService _service) =>
        {
            var res = await _service.ConfirmCode(request.Code, request.Email);
            return Results.Ok(res);
        })
        .WithName("ConfirmCode");

        userGroup.MapPost("/sign-up",
        async (UserCreateDto user, IAuthService _service) =>
            {
                return Results.Ok(await _service.SignUpUserAsync(user));
            })
            .AllowAnonymous()
        .WithName("SignUp");

        userGroup.MapPost("/login",
        async (UserLoginDto user, IAuthService _service) =>
        {
            var result = await _service.LoginUserAsync(user);
            return Results.Ok(new { success = true, data = result });
        })
         .WithName("Login");


        userGroup.MapPut("/refresh-token",
        async (RefreshRequestDto refresh, IAuthService _service) =>
        {
            return Results.Ok(await _service.RefreshTokenAsync(refresh));
        })
        .WithName("RefreshToken");


        userGroup.MapDelete("/log-out",
        async (string refreshToken, IAuthService _service) =>
        {
            await _service.LogOut(refreshToken);
            return Results.Ok();
        })
        .WithName("LogOut");

    }
}
