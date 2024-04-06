using Riok.Mapperly.Abstractions;
using Server.Models;
using Server.UseCases.Client;

namespace Server.Mappers;

[Mapper]
public partial class ClientDtoMapper
{
    public partial ClientDto MapClientToClientDto(Client client);
}