using MsVendas.Domain.Models;

namespace MsVendas.Application.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> CreatePedidoAsync(Pedido pedido, CancellationToken cancellationToken = default);
        Task<Pedido?> GetPedidoByIdAsync(Guid pedidoId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Pedido>> GetPedidosByClienteIdAsync(Guid clienteId, CancellationToken cancellationToken = default);
        Task UpdatePedidoAsync(Pedido pedido, CancellationToken cancellationToken = default);
    }
}
