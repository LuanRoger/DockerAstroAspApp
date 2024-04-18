using Server.Models.Requests;
using Server.Models.Responses;

namespace Server.Controllers.Interfaces;

public interface IClientController
{
    public Task<IEnumerable<ClientResponse>> GetAllClientsWithPagination(GetAllClientsWithPaginationRequest request);
    public Task<ClientResponse> CreateNewClient(CreateNewClientRequest request);
    public Task<ClientResponse> UpdateClient(int clientId, UpdateClientRequest request);
    public Task<int> DeleteClient(DeleteClientRequest request);
}