using MsVendas.Application.DTOs;
using MsVendas.Application.Interfaces;
using MsVendas.Domain.Models.Enums;

namespace MsVendas.Application.Controllers
{
    public static class PedidoEndpointsExtensions
    {
        public static void AddPedidosRoutes(this WebApplication app)
        {
            var pedidosGroup = app.MapGroup("/pedidos")
                .RequireAuthorization();

            var pedidosInternoGroup = app.MapGroup("/api/interno/pedidos");

            pedidosGroup.MapPost("/comprar", async (PedidoRequest pedidoRequest,
                IPedidoService pedidoService,
                CancellationToken cancellationToken) =>
            {
                var pedidoId = Guid.NewGuid();
                var clienteId = Guid.Empty;

                var result = await pedidoService.ProcessarPedidoAsync(clienteId, pedidoId, pedidoRequest, cancellationToken);
                return Results.Ok(result);
            });

            pedidosGroup.MapGet("/", async (IPedidoRepository pedidoRepository,
                IHttpContextAccessor httpContextAccessor) =>
            {
                var clienteIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst("ClienteId")?.Value;
                if (string.IsNullOrEmpty(clienteIdClaim) || !Guid.TryParse(clienteIdClaim, out var clienteId))
                {
                    return Results.Unauthorized();
                }

                var pedidos = await pedidoRepository.GetPedidosByClienteIdAsync(clienteId);
                return Results.Ok(pedidos);
            });

            pedidosGroup.MapGet("/{id}", async (Guid id, IPedidoRepository pedidoRepository,
                IHttpContextAccessor httpContextAccessor) =>
            {
                var clienteIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst("ClienteId")?.Value;
                if (string.IsNullOrEmpty(clienteIdClaim) || !Guid.TryParse(clienteIdClaim, out var clienteId))
                {
                    return Results.Unauthorized();
                }

                var pedido = await pedidoRepository.GetPedidoByIdAsync(id);
                if (pedido == null || pedido.ClienteId != clienteId)
                {
                    return Results.NotFound();
                }

                return Results.Ok(pedido);
            });

            pedidosInternoGroup.MapPut("/{id}/status", async (Guid id, AtualizarStatusPedidoRequest request,
                IPedidoService pedidoService, CancellationToken cancellationToken) =>
            {
                if (!Enum.IsDefined(typeof(PedidoStatus), request.NovoStatus))
                {
                    return Results.BadRequest($"Status inválido: {request.NovoStatus}");
                }

                var novoStatus = (PedidoStatus)request.NovoStatus;
                var resultado = await pedidoService.AtualizarStatusPedidoAsync(id, novoStatus, cancellationToken);

                if (!resultado)
                {
                    return Results.BadRequest("Não foi possível atualizar o status do pedido");
                }

                return Results.Ok(new { Message = $"Status do pedido {id} atualizado para {novoStatus}" });
            });
        }
    }
}