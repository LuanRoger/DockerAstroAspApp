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
            name = "Client 1",
            email = "client1@client.com"
        },
        new Client
        {
            id = 2,
            name = "Client 2",
            email = "client2@client.com"
        },
        new Client
        {
            id = 3,
            name = "Client 3",
            email = "client3@client.com"
        });
    }
}