namespace MSEstoque.Application.DTOs
{
    public record ProdutoRequest(string Name, decimal Preco, int Quantidade, string ProdutoDescricao);
}
