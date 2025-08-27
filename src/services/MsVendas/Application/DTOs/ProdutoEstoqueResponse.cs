namespace MsVendas.Application.DTOs
{
    public record ProdutoEstoqueResponse(Guid ProdutoId, bool IsDisponivel, int QuantidadeDisponivel, int QuantidadeSolicitada, decimal PrecoUnitario);
}
