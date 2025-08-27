using MassTransit;
using MsVendas.Application.DTOs;
using MsVendas.Application.Handlers;
using MsVendas.Domain.Models;
using shared.Events;

namespace MsVendas.Application.Controllers
{
    public static class MapPedidosRoutes
    {
        public static void AddPedidosRoutes(this WebApplication app)
        {
            app.MapPost("/pedidos", async (PedidoRequest pedidoRequest,
                PedidoHandler handler,
                IPublishEndpoint publish, CancellationToken cancellationToken) =>
            {   
                var pedidoId = Guid.NewGuid();
                // Não se esquecer de configurar Injecao de dependencia para 
                var result = await handler.HandleAsync(pedidoRequest, cancellationToken);

                // corrigir retorno depois, e verificar build

                return Results.Ok(result);
            });
        }
    }
} 