using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Endpoints;
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

builder.Services.AddMappers()
    .AddValidators()
    .AddRepositories()
    .AddUseCases()
    .AddAppController();

WebApplication app = builder.Build();

app.UseCors();

using (IServiceScope serviceScope = app.Services.CreateScope())
{
    AppDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

RouteGroupBuilder userGroup = app.MapGroup("user");
userGroup.MapClientEndpoints();

app.Run();