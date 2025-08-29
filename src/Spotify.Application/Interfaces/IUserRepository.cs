﻿using Spotify.Domain.Entities;

namespace Spotify.Application.Interfaces;

public interface IUserRepository
{
    Task<long> AddUserAsync(User user);
    Task<User> GetUserByIdAsync(long id);
    Task UpdateUserAsync(User user);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByUserNameAsync(string userName);
    Task UpdateUserRoleAsync(long userId, string userRole);
    Task DeleteUserByIdAsync(long userId);
}