using Microsoft.AspNetCore.Mvc;
using Server.Controllers.Exceptions;
using Server.Controllers.Interfaces;
using Server.Models.Requests;
using Server.Models.Responses;

namespace Server.Endpoints;

public static class ClientEndpoints
{
    public static RouteGroupBuilder MapClientEndpoints(this RouteGroupBuilder builer)
    {
        builer.MapGet("/", GetAllClientsWithPagination);
        builer.MapPost("/", CreateNewClient);
        builer.MapDelete("/{id:int}", DeleteClient);

        return builer;
    }

    async private static Task<IResult> CreateNewClient(HttpContext _,
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
        
        return Results.Created($"/user/{response.email}", response);
    }
    async private static Task<IResult> GetAllClientsWithPagination(HttpContext context, 
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
    
    async private static Task<IResult> DeleteClient(HttpContext context,
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