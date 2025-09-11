using DesafioTecnicoEcommerce.ApiGateway.Application.Interfaces;
using DesafioTecnicoEcommerce.ApiGateway.Infrastructure.Data;
using DesafioTecnicoEcommerce.ApiGateway.Infrastructure.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var jwtKey = builder.Configuration["JWT"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton<ITokenService>(sp =>
    new TokenService(jwtKey));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Cliente", policy => policy.RequireRole("Cliente"));
    options.AddPolicy("Empresa", policy => policy.RequireRole("Empresa"));
});

builder.Services.AddHttpContextAccessor();

builder.AddNpgsqlDbContext<GatewayDbContext>("ecommerceAuth");

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


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
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger();

}

app.MapReverseProxy();

app.MapDefaultEndpoints();

app.Run();
