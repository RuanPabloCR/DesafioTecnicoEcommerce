using MassTransit;
using MsVendas.Application.DTOs;
using MsVendas.Application.Interfaces;
using MsVendas.Application.Services;
using MsVendas.Domain.Models;
using MsVendas.Domain.Models.Enums;
using shared.Events;

namespace MsVendas.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly ILogger<PedidoService> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ProdutosEstoqueService _produtosEstoqueService;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PedidoService(
            ILogger<PedidoService> logger,
            IPublishEndpoint publishEndpoint,
            ProdutosEstoqueService produtosEstoqueService,
            IPedidoRepository pedidoRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _produtosEstoqueService = produtosEstoqueService;
            _pedidoRepository = pedidoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProdutosVerificadosResponse> ProcessarPedidoAsync(Guid clienteId, Guid pedidoId, PedidoRequest pedidoRequest, CancellationToken cancellationToken = default)
        {

            if (clienteId == Guid.Empty)
            {
                var clienteIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("ClienteId")?.Value;
                if (!string.IsNullOrEmpty(clienteIdClaim) && Guid.TryParse(clienteIdClaim, out var clienteIdFromToken))
                {
                    clienteId = clienteIdFromToken;
                }
                else
                {
                    _logger.LogError("ClienteId não encontrado no token JWT");
                    throw new UnauthorizedAccessException("ClienteId não encontrado no token");
                }
            }

            if (pedidoRequest.Itens == null || !pedidoRequest.Itens.Any())
            {
                _logger.LogWarning("O pedido não contém itens.");
                throw new ArgumentException("O pedido deve conter pelo menos um item");
            }

            var produtosVerificados = await _produtosEstoqueService.VerificarProdutosEstoqueAsync(pedidoRequest.Itens, cancellationToken);
            var produtosDisponiveis = produtosVerificados
                .Where(p => p.IsDisponivel && p.QuantidadeSolicitada <= p.QuantidadeDisponivel)
                .ToList();

            _logger.LogInformation("Produtos disponíveis: {produtosDisponiveis}", produtosDisponiveis);

            var produtosNaoDisponiveis = produtosVerificados.Where(p => !p.IsDisponivel || p.QuantidadeSolicitada > p.QuantidadeDisponivel)
                .ToList();

            _logger.LogInformation("Produtos não disponíveis: {produtosNaoDisponiveis}", produtosNaoDisponiveis);

            decimal total = produtosDisponiveis.Sum(produto => produto.PrecoUnitario * produto.QuantidadeSolicitada);

            List<PedidoItemEvent> pedidos = produtosDisponiveis
                .Select(p => new PedidoItemEvent(p.ProdutoId, p.QuantidadeSolicitada, p.PrecoUnitario))
                .ToList();

            _logger.LogInformation("Total do pedido calculado: {total}", total);

            var pedido = new Pedido(pedidoId, clienteId, PedidoStatus.confirmado, DateTime.UtcNow, new List<Produto>());
            await _pedidoRepository.CreatePedidoAsync(pedido, cancellationToken);

            _logger.LogInformation("Pedido {pedidoId} salvo no banco local", pedidoId);
            _logger.LogInformation("Publicando evento PedidoRealizadoEvent para o pedidoId: {pedidoId}", pedidoId);

            await _publishEndpoint.Publish(new PedidoRealizadoEvent(pedidoId, clienteId, total, pedidos), cancellationToken);
            _logger.LogInformation("Evento PedidoRealizadoEvent publicado com sucesso para o pedidoId: {pedidoId}", pedidoId);

            return new ProdutosVerificadosResponse(produtosDisponiveis, produtosNaoDisponiveis, total);
        }

        public async Task<bool> AtualizarStatusPedidoAsync(Guid pedidoId, PedidoStatus novoStatus, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Atualizando status do pedido {PedidoId} para {NovoStatus}", pedidoId, novoStatus);
                
                var pedido = await _pedidoRepository.GetPedidoByIdAsync(pedidoId, cancellationToken);
                
                if (pedido == null)
                {
                    _logger.LogWarning("Pedido {PedidoId} não encontrado", pedidoId);
                    return false;
                }
                
                if (!ValidarTransicaoStatus(pedido.Status, novoStatus))
                {
                    _logger.LogWarning("Transição de status inválida: de {StatusAtual} para {NovoStatus}", 
                        pedido.Status, novoStatus);
                    return false;
                }
                
                pedido.Status = novoStatus;
                await _pedidoRepository.UpdatePedidoAsync(pedido, cancellationToken);
                
                _logger.LogInformation("Status do pedido {PedidoId} atualizado com sucesso para {NovoStatus}", 
                    pedidoId, novoStatus);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status do pedido {PedidoId}", pedidoId);
                return false;
            }
        }
        
        private bool ValidarTransicaoStatus(PedidoStatus statusAtual, PedidoStatus novoStatus)
        {
            
            if (statusAtual == PedidoStatus.cancelado)
            {
                return false;
            }
            if (statusAtual == PedidoStatus.entregue && 
                (novoStatus == PedidoStatus.confirmado || novoStatus == PedidoStatus.enviado))
            {
                return false;
            }
            
            if (statusAtual == PedidoStatus.enviado && novoStatus == PedidoStatus.confirmado)
            {
                return false;
            }
            
            return true;
        }
    }
}
