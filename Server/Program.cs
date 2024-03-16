using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Models;
using Server.Models.Requests;
using Server.Models.Responses;
using Server.Utils;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    string connectionString = Env.GetConnectionString ?? 
                              string.Empty;
    optionsBuilder.UseNpgsql(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

WebApplication app = builder.Build();

app.UseCors();

using (IServiceScope serviceScope = app.Services.CreateScope())
{
    AppDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

RouteGroupBuilder userGroup = app.MapGroup("user");
userGroup.MapGet("/", (HttpContext _, 
    [FromQuery] int page, 
    [FromQuery] int pageSize,
    [FromServices] AppDbContext dbContext) =>
{
    var users = dbContext.users
        .AsNoTracking()
        .Skip(pageSize * (page - 1)).Take(pageSize)
        .OrderBy(f => f.id)
        .ToList();
    
    return Results.Ok(users);
});
userGroup.MapGet("/{id:int}", (HttpContext _,
    [FromRoute] int id,
    [FromServices] AppDbContext dbContext) =>
{
    User? user = dbContext.users.Find(id);
    if(user is null)
        return Results.NotFound();

    UserResponse response = new()
    {
        name = user.name,
        email = user.email
    };
    
    return Results.Ok(response);
});
userGroup.MapPost("/", (
    HttpContext _, 
    [FromBody] PostUserRequest body,
    [FromServices] AppDbContext dbContext) =>
{
    if(string.IsNullOrEmpty(body.email) || string.IsNullOrEmpty(body.name))
        return Results.BadRequest("Name and email are required");

    User newUser = new()
    {
        name = body.name,
        email = body.email
    };

    dbContext.users.Add(newUser);
    dbContext.SaveChanges();
    
    return Results.Created($"/user/{newUser.id}", newUser);
});

app.Run();