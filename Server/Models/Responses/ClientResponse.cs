namespace Server.Models.Responses;

public class ClientResponse
{
    public required int id { get; init; }
    public required string name { get; init; }
    public required string email { get; init; }
}