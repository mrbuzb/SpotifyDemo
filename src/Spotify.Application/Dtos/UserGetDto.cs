﻿namespace Spotify.Application.Dtos;

public class UserGetDto
{
    public long UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
