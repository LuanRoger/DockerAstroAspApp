namespace Server.Models.Requests;

public class GetAllClientsWithPaginationRequest
{
    public required int page { get; init; }
    public required int pageSize { get; set; }
    
    public void Deconstruct(out int oPage, out int oPageSize)
    {
        oPage = page;
        oPageSize = pageSize;
    }
}