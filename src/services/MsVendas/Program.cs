using MassTransit;
using MsVendas.Application.Handlers;
using shared.Events;
using shared.messaging;

namespace MsVendas
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();

            builder.Services.AddControllers();
            
            builder.Services.AddOpenApi();
            builder.Services.AddRabbitMqMessaging(builder.Configuration, cfg =>
            {
                //cfg.AddPublishMessage<PedidoRealizadoEvent>();
            });

            builder.Services.AddHttpClient<Infrastructure.EstoqueHttpClient>();
            builder.Services.AddScoped<Application.Services.ProdutosEstoqueService>();
            builder.Services.AddScoped<PedidoHandler>();

            var app = builder.Build();

            app.MapDefaultEndpoints();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}