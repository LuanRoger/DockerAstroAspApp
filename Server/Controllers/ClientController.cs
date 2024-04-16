using FluentValidation;
using FluentValidation.Results;
using Server.Controllers.Interfaces;
using Server.Exceptions;
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
    private readonly IRequest<int, DeleteClientCommand> _deleteClient;
    private readonly IRequest<ClientDto?, UpdateClientCommand> _updateClient;
    
    private readonly ClientResponseMapper _clientResponseMapper;
    private readonly IValidator<CreateNewClientRequest> _createNewClientValidator;
    private readonly IValidator<UpdateClientRequest> _updateClientRequestValidator;

    public ClientController(IRequest<IEnumerable<ClientDto>, GetAllClientsQuery> getAllClients,
        IRequest<ClientDto, CreateNewClientCommand> createNewClient, IRequest<int, DeleteClientCommand> deleteClient,
        ClientResponseMapper clientResponseMapper,
        IValidator<CreateNewClientRequest> createNewClientValidator, IValidator<UpdateClientRequest> updateClientRequestValidator,
        IRequest<ClientDto?, UpdateClientCommand> updateClient)
    {
        _getAllClients = getAllClients;
        _createNewClient = createNewClient;
        _deleteClient = deleteClient;
        _updateClient = updateClient;
        _clientResponseMapper = clientResponseMapper;
        _createNewClientValidator = createNewClientValidator;
        _updateClientRequestValidator = updateClientRequestValidator;
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

    public async Task<ClientResponse> UpdateClient(int clientId, UpdateClientRequest request)
    {
        ValidationResult validationResult = await _updateClientRequestValidator.ValidateAsync(request);
        if(!validationResult.IsValid)
        {
            throw new InvalidRequestException(typeof(UpdateClientRequest),
                validationResult.Errors.FormatToString());
        }
        
        (string newName, string newEmail) = request;
        UpdateClientCommand command = new(clientId, newName, newEmail);
        
        ClientDto? updatedClient = await _updateClient.Handle(command);
        if(updatedClient is null)
            throw new ClientNotFoundException(clientId);

        ClientResponse response = _clientResponseMapper.MapClientDtoToClientRequest(updatedClient);
        return response;
    }
    
    public async Task<int> DeleteClient(DeleteClientRequest request)
    {
        int deletedUserId = await _deleteClient.Handle(new(request.clientId));
        return deletedUserId;
    }
}