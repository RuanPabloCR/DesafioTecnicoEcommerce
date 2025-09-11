using MsVendas.Application.DTOs;
using MsVendas.Domain.Models.Enums;

namespace MsVendas.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<ProdutosVerificadosResponse> ProcessarPedidoAsync(Guid clienteId, Guid pedidoId, PedidoRequest pedidoRequest, CancellationToken cancellationToken = default);
        Task<bool> AtualizarStatusPedidoAsync(Guid pedidoId, PedidoStatus novoStatus, CancellationToken cancellationToken = default);
    }
}
