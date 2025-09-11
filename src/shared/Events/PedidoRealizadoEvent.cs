namespace shared.Events
{
    public class PedidoRealizadoEvent
    {
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime Data { get; private set; }
        public List<PedidoItemEvent> Itens { get; set; } = new();
        public PedidoRealizadoEvent(Guid pedidoId, Guid clienteId, decimal total, List<PedidoItemEvent> pedidos)
        {
            PedidoId = pedidoId;
            ClienteId = clienteId;
            Data = DateTime.UtcNow;
            Itens = pedidos;
        }
    }
}
