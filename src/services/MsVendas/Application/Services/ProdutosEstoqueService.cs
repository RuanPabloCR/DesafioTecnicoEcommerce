using MsVendas.Application.DTOs;
using MsVendas.Infrastructure;

namespace MsVendas.Application.Services
{
    public class ProdutosEstoqueService
    {
        private readonly EstoqueHttpClient estoqueHttpClient;
        public ProdutosEstoqueService(EstoqueHttpClient estoqueHttpClient)
        {
            this.estoqueHttpClient = estoqueHttpClient;
        }

        public async Task<List<ProdutoEstoqueResponse>> VerificarProdutosEstoqueAsync(List<ProdutoPedido> produtoPedidos, CancellationToken cancellationToken)
        {
            return await estoqueHttpClient.VerificarEstoque(produtoPedidos, cancellationToken);
        }
    }
    
}
