namespace MSEstoque.Application.DTOs
{
    public record ProdutoResponse(string Name, decimal Preco, int Quantidade, string ProdutoDescricao, Guid Id);
}
