using Riok.Mapperly.Abstractions;
using Server.Models.Responses;
using Server.UseCases.Client;

namespace Server.Mappers;

[Mapper]
public partial class ClientResponseMapper
{
    public partial ClientResponse MapClientDtoToClientRequest(ClientDto clientDto);
}