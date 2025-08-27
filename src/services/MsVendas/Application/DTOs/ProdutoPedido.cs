namespace MsVendas.Application.DTOs
{
    public record ProdutoPedido(Guid ProdutoId, int Quantidade, decimal PrecoUnitario);
}
