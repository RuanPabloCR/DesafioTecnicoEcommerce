namespace MsVendas.Application.DTOs
{
    public record ProdutosVerificadosResponse(List<ProdutoEstoqueResponse>? ProdutosDisponiveis, List<ProdutoEstoqueResponse>? ProdutosIndisponiveis);
}
