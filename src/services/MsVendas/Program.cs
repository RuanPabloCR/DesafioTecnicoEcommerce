using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MsVendas.Application.Controllers;
using MsVendas.Application.Interfaces;
using MsVendas.Application.Services;
using MsVendas.Infrastructure;
using MsVendas.Infrastructure.Data;
using MsVendas.Infrastructure.Repositories;
using shared.messaging;
using System.Text;

namespace MsVendas
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();
            var jwtKey = builder.Configuration["JWT"] ?? throw new InvalidOperationException("JWT Key not found");
            builder.Services.AddControllers();
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

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Cliente", policy => policy.RequireRole("Cliente"));
                options.AddPolicy("Empresa", policy => policy.RequireRole("Empresa"));
            });

            builder.AddNpgsqlDbContext<VendasDbContext>("ecommerceVendas");

            builder.Services.AddOpenApi();



            builder.Services.AddRabbitMqMessaging(builder.Configuration, cfg =>
            {

            });

            builder.Services.AddHttpClient<EstoqueHttpClient>();
            builder.Services.AddScoped<ProdutosEstoqueService>();

            builder.Services.AddScoped<IPedidoService, PedidoService>();
            builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            app.MapDefaultEndpoints();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.AddPedidosRoutes();
            app.MapControllers();

            app.Run();
        }
    }
}