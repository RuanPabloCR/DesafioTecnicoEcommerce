using MsVendas.Domain.Models.Enums;

namespace MsVendas.Domain.Models
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public PedidoStatus Status { get; set; }
        public ICollection<Produto> Produtos { get; private set; } = new List<Produto>();
        public DateTime CreatedAt { get; set; }
       
        public Pedido(Guid id, Guid clienteId, PedidoStatus status, DateTime createdAt, ICollection<Produto> produtos)
        {
            Id = id;
            ClienteId = clienteId;
            Status = status;
            CreatedAt = createdAt;
            Produtos = produtos;
        }
    }
}
