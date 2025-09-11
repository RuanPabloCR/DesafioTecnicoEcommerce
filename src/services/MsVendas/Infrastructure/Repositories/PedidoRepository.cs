using Microsoft.EntityFrameworkCore;
using MsVendas.Application.Interfaces;
using MsVendas.Domain.Models;
using MsVendas.Infrastructure.Data;

namespace MsVendas.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly VendasDbContext _context;

        public PedidoRepository(VendasDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> CreatePedidoAsync(Pedido pedido, CancellationToken cancellationToken = default)
        {
            await _context.Pedidos.AddAsync(pedido, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return pedido;
        }

        public async Task<Pedido?> GetPedidoByIdAsync(Guid pedidoId, CancellationToken cancellationToken = default)
        {
            return await _context.Pedidos
                .FirstOrDefaultAsync(p => p.Id == pedidoId, cancellationToken);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByClienteIdAsync(Guid clienteId, CancellationToken cancellationToken = default)
        {
            return await _context.Pedidos
                .Where(p => p.ClienteId == clienteId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdatePedidoAsync(Pedido pedido, CancellationToken cancellationToken = default)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
