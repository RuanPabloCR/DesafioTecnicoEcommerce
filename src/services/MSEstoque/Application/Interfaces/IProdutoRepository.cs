using MSEstoque.Domain.Models;

namespace MSEstoque.Application.Interfaces
{
    public interface IProdutoRepository
    {
        Task RegisterProduto(Produto produtoRequest);
        Task<IList<Produto>> GetProdutos(int page, int pageSize);
    }
}
