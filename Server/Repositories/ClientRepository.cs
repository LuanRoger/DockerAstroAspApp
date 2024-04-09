using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Models;

namespace Server.Repositories;

public class ClientRepository(AppDbContext dbContext) : IClientRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Client>> GetAllClientsWithPagination(int page, int pageSize)
    {
        var clients = await _dbContext.clients
            .AsNoTracking()
            .Skip(pageSize * (page - 1)).Take(pageSize)
            .OrderBy(f => f.id)
            .ToListAsync();

        return clients;
    }
    
    public async Task<Client?> GetClientById(int id) => 
        await _dbContext.clients.FindAsync(id);
    
    public async Task<Client> CreateNewClient(Client client) => 
        (await _dbContext.clients.AddAsync(client)).Entity;

    public async Task<int> DeleteClientById(int id)
    {
        Client? client = await GetClientById(id);
        if(client is null)
            return -1;

        _dbContext.clients.Remove(client);
        return id;
    }
    
    public Task FlushChanges() => 
        _dbContext.SaveChangesAsync();
}