﻿using Spotify.Domain.Entities;

namespace Spotify.Application.Interfaces;

public interface IRoleRepository
{
    Task<ICollection<User>> GetAllUsersByRoleAsync(string role);
    Task<List<UserRole>> GetAllRolesAsync();
    Task<long> GetRoleIdAsync(string role);
}