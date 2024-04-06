using Microsoft.EntityFrameworkCore;
using Server.Models;
#pragma warning disable CS8618

namespace Server.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Client> clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().HasData(new Client
        {
            id = 1,
            name = "User 1",
            email = "user1@user.com"
        },
        new Client
        {
            id = 2,
            name = "User 2",
            email = "user2@user.com"
        },
        new Client
        {
            id = 3,
            name = "User 3",
            email = "user3@user.com"
        });
    }
}