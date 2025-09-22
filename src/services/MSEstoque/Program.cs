using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MassTransit;
using shared.messaging;
using MSEstoque.Application.Consumers;
using MSEstoque.Application.Services;
using MSEstoque.Infrastructure;
using MSEstoque.Application.Controllers;
using MSEstoque.Infrastructure.Repositories;
using MSEstoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MSEstoque.Application.Interfaces;

namespace MSEstoque
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddServiceDefaults();

            builder.Services.AddOpenApi();
            var jwtKey = builder.Configuration["JWT"];
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? "segredo")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Cliente", policy => policy.RequireRole("Cliente"));
                options.AddPolicy("Empresa", policy => policy.RequireRole("Empresa"));
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ecommerceEstoque")));


            builder.Services.AddHttpClient<VendasHttpClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["Services:VendasApi"] ?? "http://msvendas:6060");
            });

            builder.Services.AddScoped<EstoqueRepository>();
            builder.Services.AddScoped<IProdutoRepository, ProdutosRepository>();

            builder.Services.AddScoped<EstoqueService>();
            builder.Services.AddScoped<PedidoStatusService>();
            builder.Services.AddScoped<ProdutoService>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddLogging();

            builder.Services.AddRabbitMqMessaging(builder.Configuration, cfg =>
            {
                cfg.AddConsumer<PedidoRealizadoConsumer>();
            });

            var app = builder.Build();

            app.MapDefaultEndpoints();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            // Rotas
            app.AddProdutosRoutes();

            app.Run();
        }
    }
}