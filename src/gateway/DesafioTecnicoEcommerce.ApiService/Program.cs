using DesafioTecnicoEcommerce.ApiGateway.Data;
using DesafioTecnicoEcommerce.ApiGateway.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<GatewayDbContext>("ecommerceAuth");

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<GatewayDbContext>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    await Task.Delay(5000);
    var dbContext = scope.ServiceProvider.GetRequiredService<GatewayDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger();

}

app.MapReverseProxy();

app.MapGet("/hello-world", () =>
{
    return Results.Ok("Hello, World!");
})
.WithName("Hello");

app.MapIdentityApi<User>();

app.MapDefaultEndpoints();

app.Run();
