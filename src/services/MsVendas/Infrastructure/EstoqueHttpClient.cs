using MsVendas.Application.DTOs;
using System.Text.Json;

namespace MsVendas.Infrastructure
{
    public class EstoqueHttpClient
    {
        public HttpClient httpClient { get; }
        public EstoqueHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<ProdutoEstoqueResponse>> VerificarEstoque(List<ProdutoPedido> produtoPedidos,
            CancellationToken cancellationToken)
        {
            var jsonContent = JsonSerializer.Serialize(produtoPedidos);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/estoque/disponibilidade", content, cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<List<ProdutoEstoqueResponse>>(responseContent)!;
        }
    }
}
