using Server.Mappers;
using Server.Repositories;
using Server.UseCases.Intefaces;

namespace Server.UseCases.Client;

public record CreateNewClientCommand(string name, string email);

public class CreateNewClient : IRequest<ClientDto, CreateNewClientCommand>
{
    private readonly IClientRepository _clientRepository;
    private readonly ClientMapper _clientMapper;
    private readonly ClientDtoMapper _clientDtoMapper;

    public CreateNewClient(IClientRepository clientRepository,
        ClientMapper clientMapper,
        ClientDtoMapper clientDtoMapper)
    {
        _clientRepository = clientRepository;
        _clientMapper = clientMapper;
        _clientDtoMapper = clientDtoMapper;
    }
    
    public async Task<ClientDto> Handle(CreateNewClientCommand request)
    {
        Models.Client client = _clientMapper.MapCreateNewClientCommandToClient(request);

        client = await _clientRepository.CreateNewClient(client);
        await _clientRepository.FlushChanges();

        ClientDto result = _clientDtoMapper.MapClientToClientDto(client);
        return result;
    }
}