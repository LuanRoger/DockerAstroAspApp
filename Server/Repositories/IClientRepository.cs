using Server.Models;

namespace Server.Repositories;

public interface IClientRepository
{
    public Task<IEnumerable<Client>> GetAllClientsWithPagination(int page, int pageSize);
    public Task<Client?> GetClientById(int id);
    public Task<Client> CreateNewClient(Client client);
    public Task<int> DeleteClientById(int id);
    public Task FlushChanges();
}