using MSEstoque.Infrastructure.Repositories;

namespace MSEstoque.Application.Services
{
    public class EstoqueService
    {
        private readonly EstoqueRepository _estoqueRepository;
        private readonly ILogger<EstoqueService> _logger;

        public EstoqueService(EstoqueRepository estoqueRepository, ILogger<EstoqueService> logger)
        {
            _estoqueRepository = estoqueRepository;
            _logger = logger;
        }

        public async Task<bool> AtualizarEstoqueProduto(Guid produtoId, int quantidade)
        {
            try
            {
                var result = await _estoqueRepository.AtualizarEstoque(produtoId, quantidade);
                if (!result)
                {
                    _logger.LogWarning("Falha ao atualizar estoque para o produto {ProdutoId} com quantidade {Quantidade}. Produto não encontrado ou estoque insuficiente.", produtoId, quantidade);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar estoque para o produto {ProdutoId} com quantidade {Quantidade}.", produtoId, quantidade);
                return false;
            }
        }
    }
}
