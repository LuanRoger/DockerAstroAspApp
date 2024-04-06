using Riok.Mapperly.Abstractions;
using Server.Models;
using Server.UseCases.Client;

namespace Server.Mappers;

[Mapper]
public partial class ClientMapper
{
    public partial Client MapCreateNewClientCommandToClient(CreateNewClientCommand command);
}