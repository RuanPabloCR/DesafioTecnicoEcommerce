using MsVendas.Domain.Models.Enums;

namespace MsVendas.Domain.Models
{
    public class Venda
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Guid ProdutoId { get; set; }
        public DateTime RequestedAt { get; set; }
        public Produto Produto { get; set; }

        public Venda(Produto produto, Guid clienteId, Guid produtoId)
        {
            Id = Guid.NewGuid();
            RequestedAt = DateTime.UtcNow;
            Produto = produto;
            ClienteId = clienteId;
            ProdutoId = produtoId;
        }
    }
}
 