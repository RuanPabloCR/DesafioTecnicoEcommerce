namespace shared.Events
{
    public class PedidoItemEvent
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public PedidoItemEvent(Guid produtoId, int quantidade, decimal precoUnitario)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }
    }
}
