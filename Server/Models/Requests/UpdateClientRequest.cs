namespace Server.Models.Requests;

public class UpdateClientRequest
{
    public required string? newName { get; init; }
    public required string? newEmail { get; init; }

    public void Deconstruct(out string outNewName, out string outNewEmail)
    {
        outNewName = newName;
        outNewEmail = newEmail;
    }
}