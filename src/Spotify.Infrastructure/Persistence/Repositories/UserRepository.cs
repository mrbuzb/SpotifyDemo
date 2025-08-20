using Microsoft.EntityFrameworkCore;
using Spotify.Application.Interfaces;
using Spotify.Core.Errors;
using Spotify.Domain.Entities;

namespace Spotify.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext _context) : IUserRepository
{
    public async Task<long> AddUserAync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.UserId;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _context.Users.Include(_ => _.Confirmer).FirstOrDefaultAsync(x => x.Confirmer!.Email == email);
        return user;
    }

    public async Task<long?> CheckEmailExistsAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(_ => _.Confirmer!.Email == email);
        if (user is null)
        {
            return null;
        }
        return user.UserId;
    }


    public async Task DeleteUserByIdAsync(long userId)
    {
        var user = await GetUserByIdAync(userId);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetUserByIdAync(long id)
    {
        var user = await _context.Users.Include(_ => _.Confirmer).Include(_ => _.Role).FirstOrDefaultAsync(x => x.UserId == id);
        if (user == null)
        {
            throw new EntityNotFoundException($"Entity with {id} not found");
        }
        return user;
    }

    public async Task<User> GetUserByUserNameAync(string userName)
    {
        var user = await _context.Users.Include(_ => _.Confirmer).Include(_ => _.Role).FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null)
        {
            throw new EntityNotFoundException($"Entity with {userName} not found");
        }
        return user;
    }

    public async Task AddConfirmer(UserConfirme confirmer)
    {
        await _context.Confirmers.AddAsync(confirmer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserRoleAsync(long userId, string userRole)
    {
        var user = await GetUserByIdAync(userId);
        var role = await _context.UserRoles.FirstOrDefaultAsync(x => x.Name == userRole);
        if (role == null)
        {
            throw new EntityNotFoundException($"Role : {userRole} not found");
        }
        user.RoleId = role.Id;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
