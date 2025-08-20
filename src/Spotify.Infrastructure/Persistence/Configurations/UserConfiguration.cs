﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotify.Domain.Entities;

namespace Spotify.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserName).HasMaxLength(100).IsRequired();
        builder.Property(u => u.Password).IsRequired();
        builder.Property(u => u.Salt).IsRequired();
        builder.Property(u => u.ConfirmerId).IsRequired(false);

        builder.HasOne(u => u.Confirmer)
            .WithOne(c => c.User)
            .HasForeignKey<User>(u => u.ConfirmerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Role)
               .WithMany(r => r.Users)
               .HasForeignKey(u => u.RoleId)
               .OnDelete(DeleteBehavior.Restrict);


    }
}
