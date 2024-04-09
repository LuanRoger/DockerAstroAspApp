using Server.Mappers;
using Server.Repositories;
using Server.UseCases.Intefaces;

namespace Server.UseCases.Client;

public record GetAllClientsQuery(int page, int pageSize);

public class GetAllClients : IRequest<IEnumerable<ClientDto>, GetAllClientsQuery>
{
    private readonly IClientRepository _clientRepository;
    private readonly ClientDtoMapper _clientDtoMapper;

    public GetAllClients(IClientRepository clientRepository, ClientDtoMapper clientDtoMapper)
    {
        _clientRepository = clientRepository;
        _clientDtoMapper = clientDtoMapper;
    }
    
    public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request)
    {
        var clients = await _clientRepository
            .GetAllClientsWithPagination(request.page, request.pageSize);

        var clientsDto = clients
            .Select(_clientDtoMapper.MapClientToClientDto);
        
        return clientsDto;
    }
}