namespace MsVendas.Application.DTOs
{
    public record PedidoRequest(Guid ClienteId, List<ProdutoPedido> Itens);
}
