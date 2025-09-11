using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MSEstoque.Infrastructure
{
    public class VendasHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<VendasHttpClient> _logger;

        public VendasHttpClient(HttpClient httpClient, ILogger<VendasHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> AtualizarStatusPedidoAsync(Guid pedidoId, int novoStatus, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new AtualizarStatusPedidoRequest { NovoStatus = novoStatus };
                var content = new StringContent(
                    JsonSerializer.Serialize(request),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync($"/api/interno/pedidos/{pedidoId}/status", content, cancellationToken);
                
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Status do pedido {PedidoId} atualizado com sucesso para {NovoStatus}", pedidoId, novoStatus);
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogWarning("Falha ao atualizar status do pedido {PedidoId}: {Error}", pedidoId, error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status do pedido {PedidoId}", pedidoId);
                return false;
            }
        }
    }

    public class AtualizarStatusPedidoRequest
    {
        public int NovoStatus { get; set; }
    }
}