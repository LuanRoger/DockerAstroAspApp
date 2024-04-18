using Microsoft.AspNetCore.Mvc;
using Server.Controllers.Interfaces;
using Server.Exceptions;
using Server.Models.Requests;
using Server.Models.Responses;

namespace Server.Endpoints;

public static class ClientEndpoints
{
    public static RouteGroupBuilder MapClientEndpoints(this RouteGroupBuilder builer)
    {
        builer.MapGet("/", GetAllClientsWithPagination);
        builer.MapPost("/", CreateNewClient);
        builer.MapPut("/{id:int}", UpdateClient);
        builer.MapDelete("/{id:int}", DeleteClient);

        return builer;
    }
    private static async Task<IResult> UpdateClient(HttpContext context,
        [FromRoute] int id,
        [FromBody] UpdateClientRequest body,
        [FromServices] IClientController controller)
    {
        ClientResponse response;
        try
        {
            response = await controller.UpdateClient(id, body);
        }
        catch (ClientNotFoundException)
        {
            return Results.NotFound();
        }

        return Results.Ok(response);
    }

    private static async Task<IResult> CreateNewClient(HttpContext _,
        [FromBody] CreateNewClientRequest body,
        [FromServices] IClientController controller)
    {
        ClientResponse response;
        try
        {
            response = await controller.CreateNewClient(body);
        }
        catch (InvalidRequestException e)
        {
            return Results.BadRequest(e.Message);
        }
        
        return Results.Created($"/client/{response.id}", response);
    }
    private static async Task<IResult> GetAllClientsWithPagination(HttpContext context, 
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromServices] IClientController controller)
    {
        GetAllClientsWithPaginationRequest request = new()
        {
            page = page,
            pageSize = pageSize
        };
        var response = await controller.GetAllClientsWithPagination(request);

        return Results.Ok(response);
    }
    
    private static async Task<IResult> DeleteClient(HttpContext context,
        [FromRoute] int id,
        [FromServices] IClientController controller)
    {
        int deletedClientId = await controller.DeleteClient(new()
        {
            clientId = id
        });

        return Results.Ok(deletedClientId);
    }
}