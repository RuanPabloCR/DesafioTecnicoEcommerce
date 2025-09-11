using MSEstoque.Infrastructure;
using Microsoft.Extensions.Logging;

namespace MSEstoque.Application.Services
{
    public class PedidoStatusService
    {
        private readonly VendasHttpClient _vendasHttpClient;
        private readonly ILogger<PedidoStatusService> _logger;

        public PedidoStatusService(VendasHttpClient vendasHttpClient, ILogger<PedidoStatusService> logger)
        {
            _vendasHttpClient = vendasHttpClient;
            _logger = logger;
        }

        public async Task<bool> AtualizarStatusPedidoParaEnviadoAsync(Guid pedidoId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Atualizando status do pedido {PedidoId} para Enviado", pedidoId);
            return await _vendasHttpClient.AtualizarStatusPedidoAsync(pedidoId, 1, cancellationToken);
        }

        public async Task<bool> AtualizarStatusPedidoParaEntregueAsync(Guid pedidoId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Atualizando status do pedido {PedidoId} para Entregue", pedidoId);
            return await _vendasHttpClient.AtualizarStatusPedidoAsync(pedidoId, 2, cancellationToken);
        }
    }
}