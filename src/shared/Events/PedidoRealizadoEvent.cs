namespace shared.Events
{
    public class PedidoRealizadoEvent
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Data { get; set; }
        public List<PedidoItemEvent> Itens { get; set; } = new();
    }
}
