namespace Server.Models.Requests;

public class CreateNewClientRequest
{
    public required string name { get; init; }
    public required string email { get; init; }

    public void Deconstruct(out string oName, out string oEmail)
    {
        oName = name;
        oEmail = email;
    }
}