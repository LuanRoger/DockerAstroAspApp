using Microsoft.EntityFrameworkCore;
using Server.Models;
#pragma warning disable CS8618

namespace Server.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        {
            id = 1,
            name = "User 1",
            email = "user1@user.com"
        },
        new User
        {
            id = 2,
            name = "User 2",
            email = "user2@user.com"
        },
        new User
        {
            id = 3,
            name = "User 3",
            email = "user3@user.com"
        });
    }
}