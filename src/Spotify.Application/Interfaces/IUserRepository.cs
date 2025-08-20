using Spotify.Domain.Entities;

namespace Spotify.Application.Interfaces;

public interface IUserRepository
{
    Task<long> AddUserAync(User user);
    Task<User> GetUserByIdAync(long id);
    Task UpdateUser(User user);
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserByUserNameAync(string userName);
    Task UpdateUserRoleAsync(long userId, string userRole);
    Task DeleteUserByIdAsync(long userId);
}