using Server.Mappers;
using Server.Repositories;
using Server.UseCases.Intefaces;

namespace Server.UseCases.Client;

public record UpdateClientCommand(int clientId, string? name, string? email);

public class UpdateClient : IRequest<ClientDto?, UpdateClientCommand>
{
    private readonly IClientRepository _clientRepository;
    private readonly ClientDtoMapper _clientDtoMapper;

    public UpdateClient(IClientRepository clientRepository, ClientDtoMapper clientDtoMapper)
    {
        _clientRepository = clientRepository;
        _clientDtoMapper = clientDtoMapper;
    }
    
    public async Task<ClientDto?> Handle(UpdateClientCommand request)
    {
        Models.Client? clientToUpdate = await _clientRepository.GetClientById(request.clientId);
        if(clientToUpdate is null)
            return null;

        if(request.name is not null)
            clientToUpdate.name = request.name;
        if(request.email is not null)
            clientToUpdate.email = request.email;

        await _clientRepository.FlushChanges();
        
        ClientDto clientDto = _clientDtoMapper.MapClientToClientDto(clientToUpdate);
        return clientDto;
    }
}