using FluentValidation;
using FluentValidation.Results;
using Server.Controllers.Exceptions;
using Server.Controllers.Interfaces;
using Server.Mappers;
using Server.Models.Requests;
using Server.Models.Responses;
using Server.UseCases.Client;
using Server.UseCases.Intefaces;
using Server.Utils;

namespace Server.Controllers;

public class ClientController : IClientController
{
    private readonly IRequest<IEnumerable<ClientDto>, GetAllClientsQuery> _getAllClients;
    private readonly IRequest<ClientDto, CreateNewClientCommand> _createNewClient;
    
    private readonly ClientResponseMapper _clientResponseMapper;
    private readonly IValidator<CreateNewClientRequest> _createNewClientValidator;

    public ClientController(IRequest<IEnumerable<ClientDto>, GetAllClientsQuery> getAllClients,
        IRequest<ClientDto, CreateNewClientCommand> createNewClient,
        ClientResponseMapper clientResponseMapper,
        IValidator<CreateNewClientRequest> createNewClientValidator)
    {
        _getAllClients = getAllClients;
        _createNewClient = createNewClient;
        _clientResponseMapper = clientResponseMapper;
        _createNewClientValidator = createNewClientValidator;
    }
    
    public async Task<IEnumerable<ClientResponse>> GetAllClientsWithPagination(GetAllClientsWithPaginationRequest request)
    {
        (int page, int pageSize) = request;
        GetAllClientsQuery query = new(page, pageSize);
        
        var clients = await _getAllClients.Handle(query);
        var response = clients
            .Select(_clientResponseMapper.MapClientDtoToClientRequest);

        return response;
    }

    public async Task<ClientResponse> CreateNewClient(CreateNewClientRequest request)
    {
        ValidationResult validationResult = await _createNewClientValidator.ValidateAsync(request);
        if(!validationResult.IsValid)
        {
            throw new InvalidRequestException(typeof(CreateNewClientRequest), 
                validationResult.Errors.FormatToString());
        }
        
        (string name, string email) = request;
        CreateNewClientCommand command = new(name, email);

        ClientDto clientDto = await _createNewClient.Handle(command);
        
        ClientResponse response = _clientResponseMapper.MapClientDtoToClientRequest(clientDto);
        return response;
    }
}