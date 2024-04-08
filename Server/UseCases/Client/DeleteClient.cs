using Server.Repositories;
using Server.UseCases.Intefaces;

namespace Server.UseCases.Client;

public record DeleteClientCommand(int id);

public class DeleteClient : IRequest<int, DeleteClientCommand>
{
    private readonly IClientRepository _clientRepository;

    public DeleteClient(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public async Task<int> Handle(DeleteClientCommand request)
    {
        int deleteClientId = await _clientRepository.DeleteClientById(request.id);
        await _clientRepository.FlushChanges();

        return deleteClientId;
    }
}