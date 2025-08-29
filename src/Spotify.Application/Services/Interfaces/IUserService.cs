﻿using Microsoft.AspNetCore.Http;

namespace Spotify.Application.Services.Interfaces;

public interface IUserService
{
    Task UpdateUserRoleAsync(long userId, string userRole);
    Task DeleteUserByIdAsync(long userId, string userRole);
    Task UploadProfileImgAsync(IFormFile file, long userId);
}
